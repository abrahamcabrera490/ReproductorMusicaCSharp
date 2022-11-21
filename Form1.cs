using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMusicPlayer
{
    public partial class MusicPlayer : Form
    {
        /*
        List<string> _songNames;
        List<string> _songPaths;
        */
        List<Song> _songs;
        public MusicPlayer()
        {
            InitializeComponent();
        }

        private void MusicPlayer_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            
            dialog.Multiselect = true;

            if(dialog.ShowDialog() == DialogResult.OK )
            {


                AddSongsTolist(dialog.SafeFileNames.ToList(), dialog.FileNames.ToList());

                //Añadir musica a la lista

            }


        }


        private void AddSongsTolist(List<string> names, List<string> paths)
        {

            if(_songs == null)
            {
                _songs = new List<Song>();
            }

         
            foreach (var item in names)
            {

                //Comprobamos si existe o no

                if (!ExistOnList(item))
                    _songs.Add(new Song(item, GetPath(item, paths)));
            }

            RefreshList();
        }


        private string GetPath(string fileName, List<string> songPath = null)
        {
            string actualpath = string.Empty;



        if(songPath == null )

            {

                foreach (var songs in _songs)
                {
                    if (songs.Name == fileName)
                    {
                        actualpath = songs.Path;
                    }
                }

            }
            
        else { 

            foreach(var path in songPath)
            {
                if(path.Contains(fileName))
                {
                    actualpath = path;
               
                    }
            }
            }
            return actualpath;
        }


        private bool ExistOnList(string song)
        {

            bool exist = false;
            foreach (var item in _songs)
            {
                if(item.Name == song)
                {
                  exist = true;
                }
            }
            return exist;

        }


        private void songList_DoubleClick(object sender, EventArgs e)
        {



            axWindowsMediaPlayer1.URL = GetPath(songList.Text);



        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Song songRemobe = null;

            foreach(var song in _songs)
            {
                if (song.Name == songList.Text)
                {
                    songRemobe = song;
                }
            }
            if(songRemobe != null)
                _songs.Remove(songRemobe);


            RefreshList();
        }


        private void RefreshList()
        {
            List<string> songNames = new List<string>();

            foreach(var item in _songs)
            {
                songNames.Add(item.Name);
            }
            songList.DataSource = null;
            songList.DataSource = songNames;
        }
    }
}
