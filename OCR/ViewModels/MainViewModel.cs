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
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace OCR.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Command allows user to change song directory setting. 
        /// </summary>
        public ICommand ChangeSongDirCommand => new RelayCommand(ChangeSongDir_Command);

        public ICommand PlayListSelectionCommand => new RelayCommand(PlayListSelection_Command);

        public ICommand PlayAudioCommand => new RelayCommand(PlayAudio_Command);
        public ICommand CreatePlayListCommand => new RelayCommand(CreatePlayList_Command);

        /// <summary>
        /// SongList property bound to ListView in MainWindow. 
        /// </summary>
        public ObservableCollection<Song> SongList { get; set; }

        private Song _selecteSong;
        public Song SelectedSong
        {
            get { return _selecteSong; }
            set { Set(() => SelectedSong, ref _selecteSong, value); }
        }

        /// <summary>
        /// Class initialization method. 
        /// </summary>
        public MainViewModel()
        {
            UpdateListView();

            Default.PropertyChanged += Default_PropertyChanged;

            /* Get Play-lists */
            string listsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OCRtunes",  "Playlists");
            if (!Directory.Exists(listsPath))
            {
                Directory.CreateDirectory(listsPath);
            }

            string[] playlists = Directory.GetDirectories(listsPath);

            foreach (var playlist in playlists)
            {
                List<Song> songs = new List<Song>();
                string[] songsInPath = Directory.GetFiles(playlist);
                foreach (var song in songsInPath)
                {
                    songs.Add(NewSong(song));
                }

                Playlist newPlaylist = new Playlist()
                {
                    Title = playlist.Split(Path.DirectorySeparatorChar).Last(),
                    Songs = songs
                };

                Playlist.Playlists.Add(newPlaylist);
            }
        }

        /// <summary>
        /// Clears SongList and updates it with new values when song directory is changed. 
        /// </summary>
        private void UpdateListView()
        {
            SongList = new ObservableCollection<Song>();

            string songDir = Default.SongDir;
            if (!Directory.Exists(songDir))
            {
                Directory.CreateDirectory(songDir);
            }

            var songFiles = Directory.GetFiles(songDir);
            //songFiles.Sort();

            foreach (var songFile in songFiles)
            {
                SongList.Add(NewSong(songFile));
            }
        }

        private Song NewSong(string filepath)
        {
            TagLib.File tagFile = TagLib.File.Create(filepath);

            var newSong = new Song()
            {
                FullPath = filepath,
                Title = Path.GetFileNameWithoutExtension(tagFile.Name ?? filepath),
                Artist = tagFile.Tag.Performers.Length != 0 ? tagFile.Tag.Performers[0] : "---",
                Genre = tagFile.Tag.Genres.Length != 0 ? tagFile.Tag.Genres[0] : "---",
                Length = Converter.ConvertTime(tagFile.Properties.Duration.TotalSeconds),
                Size = Converter.ConvertSize((float)new FileInfo(filepath).Length)
            };

            tagFile.Dispose();
            return newSong;
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

        private void PlayAudio_Command()
        {
            //MessageBox.Show("Playing " + SelectedSong.FullPath);
            Process.Start(SelectedSong.FullPath);
        }

        private void CreatePlayList_Command()
        {

        }

        /// <summary>
        /// Redundant void
        /// </summary>
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
