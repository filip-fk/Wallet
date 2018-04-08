using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Wallet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class passesPage : Page
    {
        public passesPage()
        {
            this.InitializeComponent();
            update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            load_one();
        }

        private async void load_one()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".pkpass");
            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                await AddPkapssFile(file);
            }
            else
            {
                //  
            }
        }
        
        private async void DisplayDialog(string title, string content, string pr_btn_txt, string type)
        {
            ContentDialog infoDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                PrimaryButtonText = pr_btn_txt,
                CloseButtonText = "Close"
            };

            ContentDialogResult result = await infoDialog.ShowAsync();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var args = e.Parameter as Windows.ApplicationModel.Activation.IActivatedEventArgs;
            if (args != null)
            {
                if (args.Kind == Windows.ApplicationModel.Activation.ActivationKind.File)
                {
                    var fileArgs = args as Windows.ApplicationModel.Activation.FileActivatedEventArgs;
                    string strFilePath = fileArgs.Files[0].Path;
                    var file = (StorageFile)fileArgs.Files[0];
                    await AddPkapssFile(file);
                }
            }
        }

        private async Task AddPkapssFile(StorageFile file)
        {
            if (file != null)
            {
                StorageFolder passes = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Passes", CreationCollisionOption.OpenIfExists);
                StorageFolder passesX = await ApplicationData.Current.LocalFolder.CreateFolderAsync("PassesX", CreationCollisionOption.OpenIfExists);
                StorageFolder passesZ = await ApplicationData.Current.LocalFolder.CreateFolderAsync("PassesZ", CreationCollisionOption.OpenIfExists);
                await file.CopyAsync(passes, "BoardingPass.pkpass", NameCollisionOption.GenerateUniqueName);

                update();
            }
            else
            {
                //cancel
            }
        }

        private async void update()
        {
            pass_list.Items.Clear();

            StorageFolder passes = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Passes", CreationCollisionOption.OpenIfExists);
            StorageFolder passesX = await ApplicationData.Current.LocalFolder.CreateFolderAsync("PassesX", CreationCollisionOption.OpenIfExists);
            StorageFolder passesZ = await ApplicationData.Current.LocalFolder.CreateFolderAsync("PassesZ", CreationCollisionOption.OpenIfExists);
            IReadOnlyList<StorageFile> ex_fol_list = await passes.GetFilesAsync();
            foreach (StorageFile ex_F in ex_fol_list)
            {
                to_zip(ex_F);
            }
        }
        
        //part of a *foreach-statement* (for eaach pass in the folder)
        private async void to_zip(StorageFile file)
        {
            StorageFolder passes = await ApplicationData.Current.LocalFolder.GetFolderAsync("PassesZ");
            string f_path = file.Path;

            string new_ext = Path.ChangeExtension(f_path, ".zip");

            StorageFile pass = await file.CopyAsync(passes, "BoardingPass.zip", NameCollisionOption.GenerateUniqueName);
            extract_f(pass);
        }

        async void extract_f(StorageFile pass)
        {
            StorageFolder passesZ = await ApplicationData.Current.LocalFolder.GetFolderAsync("PassesZ");
            StorageFolder passesX = await ApplicationData.Current.LocalFolder.GetFolderAsync("PassesX");
            StorageFile passL = await passesZ.GetFileAsync(pass.Name);

            string zipPath = passL.Path;

            StorageFolder extract = await passesX.CreateFolderAsync(passL.Name + "_extract", CreationCollisionOption.ReplaceExisting);

            string extractPath = extract.Path;

            await Task.Run(() =>
            {
                Task.Yield();
                ZipFile.ExtractToDirectory(zipPath, extractPath);
            });

            read_f(extract, extract.Name);
        }

        private async void read_f(StorageFolder passF, string fol_name)
        {
            StorageFile jsonF = await passF.GetFileAsync("pass.json");
            load_JSON_items(jsonF, fol_name);
        }

        private async void load_JSON_items(StorageFile pass, string fol_name)
        {
            //var Serializer = new DataContractSerializer(typeof(List<Pass>));
            StorageFolder ex_fol = await ApplicationData.Current.LocalFolder.GetFolderAsync("PassesX");
            StorageFolder ex_fol_L = await ex_fol.GetFolderAsync(fol_name);
            var welcome = Welcome.FromJson(await FileIO.ReadTextAsync(pass));
            update_UI(welcome);
        }

        private void update_UI(object welcome0)
        {
            var welcome = (Welcome)welcome0;
            StackPanel stack = new StackPanel();
            TextBlock text1 = new TextBlock { Text = welcome.OrganizationName };
            TextBlock text2 = new TextBlock { Text = welcome.Description };
            //....//
            stack.Children.Add(text1);
            stack.Children.Add(text2);
            pass_list.Items.Add(stack);
        }
        //until here

        private List<Welcome> Passes
        {
            get;
            set;
        } = new List<Welcome>();
    }
}