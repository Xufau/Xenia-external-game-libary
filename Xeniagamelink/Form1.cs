using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace Xeniagamelink
{

    public partial class Form1 : Form
    {
        bool fullscreen = false;
        List<string> options = new List<string>();
        List<string> games = new List<string>();
        List<string> cgames = new List<string>();
        List<string> file = new List<string>();

        string xenia = "";
        string addgamep = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void CreateShortcut( string gamepath, string xeniapath)
        {
            if (button10.ForeColor == Color.Red)
                MessageBox.Show("Select Xenia path");
            else
            {
                string currentpath = Directory.GetCurrentDirectory();
                if (System.IO.File.Exists(currentpath + @"\game.lnk"))
                {
                    System.IO.File.Delete(currentpath + @"\game.lnk");
                }


                WshShell shell = new WshShell();

                string shortcutAddress = currentpath + @"\" + "game" + @".lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                shortcut.TargetPath = xeniapath;

                if (fullscreen)
                    shortcut.Arguments = @"--fullscreen" + @" --target=" + gamepath;

                if (!fullscreen)
                    shortcut.Arguments = @"--target=" + gamepath;

                shortcut.Save();

                try
                {
                    Process.Start(currentpath + @"\game.lnk");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            writetofile();
            gengames();

            if(fullscreen)
            {
                button3.ForeColor = Color.Green;
            }
            else
            {
                button3.ForeColor = Color.Red;
            }

            if (System.IO.File.Exists(xenia))
                button10.ForeColor = Color.Green;
            else
                button10.ForeColor = Color.Red;

            //genicons();
        }

        void genicons()
        {
            /*
            Task.Run(() =>
            {
                Process[] myprocesses = Process.GetProcesses();
                try
                {
                    for (int i = 0; i < myprocesses.Length; i++)
                    {
                        if (myprocesses[i].MainWindowTitle.Contains("detached"))
                        {
                            foreach (Control x in this.Controls)
                            {
                                if (x is PictureBox && x.Tag == "gameicon" && ((PictureBox)x).Image == null)
                                {
                                    ((PictureBox)x).Image = (Image)(new Bitmap(Bitmap.FromHicon(new Icon(Icon.ExtractAssociatedIcon(myprocesses[i].MainModule.FileName), new Size(48, 48)).Handle), new Size(140, 140)));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            */
        }

        private void button2_Click(object sender, EventArgs e)//options
        {
            groupBox2.Visible = false;
            if (groupBox1.Visible)
                groupBox1.Visible = false;
            else
                groupBox1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)//fullscreen
        {
            if (fullscreen)
                fullscreen = false;
            else
                fullscreen = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Location = new Point(this.Width / 2 - (groupBox1.Size.Width/2), this.Height / 2 - (groupBox1.Size.Height/2));
            groupBox2.Location = new Point(this.Width / 2 - (groupBox2.Size.Width/2), this.Height / 2 - (groupBox2.Size.Height/2));
            readfromfile();
        }

        void writetofile()
        {
            if (System.IO.File.Exists(Directory.GetCurrentDirectory() + @"\config.txt"))
            {
                System.IO.File.Delete(Directory.GetCurrentDirectory() + @"\config.txt");
            }
            using (StreamWriter sw = System.IO.File.CreateText(Directory.GetCurrentDirectory() + @"\config.txt"))
            {
                
                //write xenia path
                sw.WriteLine(xenia);

                //write options
                if (fullscreen)
                    sw.WriteLine("options fullscreen true");
                else
                    sw.WriteLine("options fullscreen false");

                //write games
                for (int i = 0; i < games.Count(); i++)//gamename gamepath
                {
                    sw.WriteLine(games[i]);
                }
            }
        }

        public void readfromfile()
        {
            file.Clear();
            games.Clear();
            try
            {
                using (StreamReader sr = System.IO.File.OpenText(Directory.GetCurrentDirectory() + @"\config.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        if(xenia == "")
                        {
                            if (s.Contains("xenia"))
                            {
                                xenia = s;
                            }
                        }

                        if(s.Contains("options") == false && s.Contains(".exe") == false && s != "" && s != " ")
                        {
                            games.Add(s);
                        }

                        if (s.Contains("options"))
                        {
                            if (s.Contains("fullscreen"))
                            {
                                if (s.Contains("true"))
                                    fullscreen = true;

                                if (s.Contains("false"))
                                    fullscreen = false;
                            }
                        }
                    }
                }
            }
            catch{}
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[0].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[1].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[2].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[3].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[4].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[5].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[6].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[7].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[8].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[9].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[10].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[11].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[12].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[13].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[14].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[15].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[16].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[17].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[18].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[19].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        { 
            string[] gamepath = games[20].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[21].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[22].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            string[] gamepath = games[23].Split('`');
            CreateShortcut(gamepath[0], xenia);
            clickoff();
        }

        private void button10_Click(object sender, EventArgs e)//xenia path
        {
            clickoff();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\";
            ofd.Title = "Select file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                xenia = ofd.FileName;
                Console.WriteLine(xenia);
            }
        }

        void gengames()
        {
            int gamecount = games.Count();

            if(gamecount >= 1)
            {
                pictureBox3.Visible = true;
                label1.Visible = true;
                button12.Visible = true;
                string[] gamepath = games[0].Split('`');
                label1.Text = gamepath[1];
            }
            else
            {
                pictureBox3.Visible = false;
                label1.Visible = false;
                button12.Visible = false;
            }

            if (gamecount >= 2)
            {
                label2.Visible = true;
                button13.Visible = true;
                pictureBox4.Visible = true;
                string[] gamepath = games[1].Split('`');
                label2.Text = gamepath[1];
            }
            else
            {
                label2.Visible = false;
                button13.Visible = false;
                pictureBox4.Visible = false;
            }

            if (gamecount >= 3)
            {
                label3.Visible = true;
                button14.Visible = true;
                pictureBox5.Visible = true;
                string[] gamepath = games[2].Split('`');
                label3.Text = gamepath[1];
            }
            else
            {
                label3.Visible = false;
                button14.Visible = false;
                pictureBox5.Visible = false;
            }

            if (gamecount >= 4)
            {
                label4.Visible = true;
                button15.Visible = true;
                pictureBox6.Visible = true;
                string[] gamepath = games[3].Split('`');
                label4.Text = gamepath[1];
            }
            else
            {
                label4.Visible = false;
                button15.Visible = false;
                pictureBox6.Visible = false;
            }

            if (gamecount >= 5)
            {
                label5.Visible = true;
                button17.Visible = true;
                pictureBox7.Visible = true;
                string[] gamepath = games[4].Split('`');
                label5.Text = gamepath[1];
            }
            else
            {
                label5.Visible = false;
                button17.Visible = false;
                pictureBox7.Visible = false;
            }

            if (gamecount >= 6)
            {
                label6.Visible = true;
                button18.Visible = true;
                pictureBox8.Visible = true;
                string[] gamepath = games[5].Split('`');
                label6.Text = gamepath[1];
            }
            else
            {
                label6.Visible = false;
                button18.Visible = false;
                pictureBox8.Visible = false;
            }

            if (gamecount >= 7)
            {
                label7.Visible = true;
                button19.Visible = true;
                pictureBox9.Visible = true;
                string[] gamepath = games[6].Split('`');
                label7.Text = gamepath[1];
            }
            else
            {
                label7.Visible = false;
                button19.Visible = false;
                pictureBox9.Visible = false;
            }

            if (gamecount >= 8)
            {
                label8.Visible = true;
                button20.Visible = true;
                pictureBox10.Visible = true;
                string[] gamepath = games[7].Split('`');
                label8.Text = gamepath[1];
            }
            else
            {
                label8.Visible = false;
                button20.Visible = false;
                pictureBox10.Visible = false;
            }

            if (gamecount >= 9)
            {
                label9.Visible = true;
                button21.Visible = true;
                pictureBox11.Visible = true;
                string[] gamepath = games[8].Split('`');
                label9.Text = gamepath[1];
            }
            else
            {
                label9.Visible = false;
                button21.Visible = false;
                pictureBox11.Visible = false;
            }

            if (gamecount >= 10)
            {
                label10.Visible = true;
                button22.Visible = true;
                pictureBox12.Visible = true;
                string[] gamepath = games[9].Split('`');
                label10.Text = gamepath[1];
            }
            else
            {
                label10.Visible = false;
                button22.Visible = false;
                pictureBox12.Visible = false;
            }

            if (gamecount >= 11)
            {
                label11.Visible = true;
                button23.Visible = true;
                pictureBox13.Visible = true;
                string[] gamepath = games[10].Split('`');
                label11.Text = gamepath[1];
            }
            else
            {
                label11.Visible = false;
                button23.Visible = false;
                pictureBox13.Visible = false;
            }

            if (gamecount >= 12)
            {
                label12.Visible = true;
                button24.Visible = true;
                pictureBox14.Visible = true;
                string[] gamepath = games[11].Split('`');
                label12.Text = gamepath[1];
            }
            else
            {
                label12.Visible = false;
                button24.Visible = false;
                pictureBox14.Visible = false;
            }

            if (gamecount >= 13)
            {
                label13.Visible = true;
                button25.Visible = true;
                pictureBox15.Visible = true;
                string[] gamepath = games[12].Split('`');
                label13.Text = gamepath[1];
            }
            else
            {
                label13.Visible = false;
                button25.Visible = false;
                pictureBox15.Visible = false;
            }

            if (gamecount >= 14)
            {
                label14.Visible = true;
                button26.Visible = true;
                pictureBox16.Visible = true;
                string[] gamepath = games[13].Split('`');
                label14.Text = gamepath[1];
            }
            else
            {
                label14.Visible = false;
                button26.Visible = false;
                pictureBox16.Visible = false;
            }

            if (gamecount >= 15)
            {
                label15.Visible = true;
                button27.Visible = true;
                pictureBox17.Visible = true;
                string[] gamepath = games[14].Split('`');
                label15.Text = gamepath[1];
            }
            else
            {
                label15.Visible = false;
                button27.Visible = false;
                pictureBox17.Visible = false;
            }

            if (gamecount >= 16)
            {
                label16.Visible = true;
                button28.Visible = true;
                pictureBox18.Visible = true;
                string[] gamepath = games[15].Split('`');
                label16.Text = gamepath[1];
            }
            else
            {
                label16.Visible = false;
                button28.Visible = false;
                pictureBox18.Visible = false;
            }

            if (gamecount >= 17)
            {
                label17.Visible = true;
                button29.Visible = true;
                pictureBox19.Visible = true;
                string[] gamepath = games[16].Split('`');
                label17.Text = gamepath[1];
            }
            else
            {
                label17.Visible = false;
                button29.Visible = false;
                pictureBox19.Visible = false;
            }

            if (gamecount >= 18)
            {
                label18.Visible = true;
                button30.Visible = true;
                pictureBox20.Visible = true;
                string[] gamepath = games[17].Split('`');
                label18.Text = gamepath[1];
            }
            else
            {
                label18.Visible = false;
                button30.Visible = false;
                pictureBox20.Visible = false;
            }

            if (gamecount >= 19)
            {
                label19.Visible = true;
                button31.Visible = true;
                pictureBox21.Visible = true;
                string[] gamepath = games[18].Split('`');
                label19.Text = gamepath[1];
            }
            else
            {
                label19.Visible = false;
                button31.Visible = false;
                pictureBox21.Visible = false;
            }

            if (gamecount >= 20)
            {
                label20.Visible = true;
                button32.Visible = true;
                pictureBox22.Visible = true;
                string[] gamepath = games[19].Split('`');
                label20.Text = gamepath[1];
            }
            else
            {
                label20.Visible = false;
                button32.Visible = false;
                pictureBox22.Visible = false;
            }

            if (gamecount >= 21)
            {
                label21.Visible = true;
                button33.Visible = true;
                pictureBox23.Visible = true;
                string[] gamepath = games[20].Split('`');
                label21.Text = gamepath[1];
            }
            else
            {
                label21.Visible = false;
                button33.Visible = false;
                pictureBox23.Visible = false;
            }

            if (gamecount >= 22)
            {
                label22.Visible = true;
                button34.Visible = true;
                pictureBox24.Visible = true;
                string[] gamepath = games[21].Split('`');
                label22.Text = gamepath[1];
            }
            else
            {
                label22.Visible = false;
                button34.Visible = false;
                pictureBox24.Visible = false;
            }

            if (gamecount >= 23)
            {
                label23.Visible = true;
                button35.Visible = true;
                pictureBox25.Visible = true;
                string[] gamepath = games[22].Split('`');
                label23.Text = gamepath[1];
            }
            else
            {
                label23.Visible = false;
                button35.Visible = false;
                pictureBox25.Visible = false;
            }

            if (gamecount >= 24)
            {
                label24.Visible = true;
                button25.Visible = true;
                pictureBox26.Visible = true;
                string[] gamepath = games[23].Split('`');
                label24.Text = gamepath[1];
            }
            else
            {
                label24.Visible = false;
                button25.Visible = false;
                pictureBox26.Visible = false;
            }

        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            clickoff();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(groupBox2.Visible == true)
                clickoff();
            else
            {
                addgamep = "";
                textBox1.Text = "";
                groupBox2.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            clickoff();
        }
        
        void clickoff()
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(addgamep != "" && textBox1.Text != "")
            {
                string add = addgamep + "`" + textBox1.Text;
                games.Add(add);
                groupBox2.Visible = false;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\";
            ofd.Title = "Select file";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                addgamep = ofd.FileName;
                Console.WriteLine(addgamep);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Visible = false;
            games.RemoveAt(0);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Visible = false;
            games.RemoveAt(1);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button14.Visible = false;
            games.RemoveAt(2);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button15.Visible = false;
            games.RemoveAt(3);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            button17.Visible = false;
            games.RemoveAt(4);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            button18.Visible = false;
            games.RemoveAt(5);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            button19.Visible = false;
            games.RemoveAt(6);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            button20.Visible = false;
            games.RemoveAt(7);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            button21.Visible = false;
            games.RemoveAt(8);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            button22.Visible = false;
            games.RemoveAt(9);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            button23.Visible = false;
            games.RemoveAt(10);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            button24.Visible = false;
            games.RemoveAt(11);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            button25.Visible = false;
            games.RemoveAt(12);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            button26.Visible = false;
            games.RemoveAt(13);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            button27.Visible = false;
            games.RemoveAt(14);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            button28.Visible = false;
            games.RemoveAt(15);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            button29.Visible = false;
            games.RemoveAt(16);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            button30.Visible = false;
            games.RemoveAt(17);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            button31.Visible = false;
            games.RemoveAt(18);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            button32.Visible = false;
            games.RemoveAt(19);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            button33.Visible = false;
            games.RemoveAt(20);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            button34.Visible = false;
            games.RemoveAt(21);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            button35.Visible = false;
            games.RemoveAt(22);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            button36.Visible = false;
            games.RemoveAt(23);
        }
    }
}
