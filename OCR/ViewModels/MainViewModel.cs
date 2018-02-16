using GalaSoft.MvvmLight;
using OCR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;
using System.Windows;
using static OCR.Properties.Settings;
using OCR.Services;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using TagLib.Id3v2;

namespace OCR.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Command allows user to change song directory setting. 
        /// </summary>
        public ICommand ChangeSongDirCommand => new RelayCommand(ChangeSongDir_Command);

        public ICommand PlayListSelectionCommand => new RelayCommand(PlayListSelection_Command);

        /// <summary>
        /// SongList property bound to ListView in MainWindow. 
        /// </summary>
        private List<Song> _songList;
        public List<Song> SongList
        {
            get { return _songList; }
            set { Set(() => SongList, ref _songList, value); }
        }

        /// <summary>
        /// Class initialization method. 
        /// </summary>
        public MainViewModel()
        {
            UpdateListView();

            Default.PropertyChanged += Default_PropertyChanged;
        }

        /// <summary>
        /// Clears SongList and updates it with new values when song directory is changed. 
        /// </summary>
        private void UpdateListView()
        {
            SongList = new List<Song>();

            string songDir = Default.SongDir;
            if (!Directory.Exists(songDir))
            {
                Directory.CreateDirectory(songDir);
            }

            List<string> songFiles = Directory.GetFiles(songDir, "*.mp3").ToList<string>();
            songFiles.Sort();

            foreach (var songFile in songFiles)
            {
                TagLib.File tagFile = TagLib.File.Create(songFile);

                TagLib.Id3v2.Tag tags = (TagLib.Id3v2.Tag)tagFile.GetTag(TagTypes.Id3v2);
                PrivateFrame pFrame = PrivateFrame.Get(tags, "Artist", true);
                pFrame.PrivateData = Encoding.Unicode.GetBytes("Mishary Rashid Alafasy");
                tagFile.Save();
                //tagFile.Tag.Performers.Append("Mishary Rashid Alafasy");
                //tagFile.Save();

                SongList.Add(new Song()
                {
                    Title = Path.GetFileNameWithoutExtension(tagFile.Name ?? songFile),
                    Artist = tagFile.Tag.Performers.Length != 0 ? tagFile.Tag.Performers[0] : "---",
                    Genre = tagFile.Tag.Genres.Length != 0 ? tagFile.Tag.Genres[0] : "---",
                    Length = Converter.ConvertTime(tagFile.Properties.Duration.TotalSeconds),
                    Size = Converter.ConvertSize((float)new FileInfo(songFile).Length)
                });

                tagFile.Dispose();
            }
        }

        /// <summary>
        /// This method will be called when settings are changed or reset. 
        /// </summary>
        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateListView();

            //This will save the song directory so that settings stay the same on application restart. 
            Default.Save();
        }


        private void PlayListSelection_Command()
        {

        }

        private void ChangeSongDir_Command()
        {
            string dir = FolderExtention.GetFolder(Default.SongDir);

            Default.SongDir = dir ?? Default.SongDir;
        }
    }
}
