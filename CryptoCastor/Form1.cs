using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoCastor
{
    public partial class Form1 : Form
    {
        private List<Conteneur> conteneurs;
        private Crypto crypto;
        private PassWordValider pwv;

        public Form1()
        {
            InitializeComponent();
            crypto = new Crypto();
            pwv = new PassWordValider();
            conteneurs = new List<Conteneur>();
        }

        public void UpdateAllDataList()
		{
            inputDataList.Items.Clear();
            foreach (Conteneur c in conteneurs)
			{
                inputDataList.Items.Add(c);
			}          
		}

        #region Evenements
        private void buttonAdd_Click(object sender, EventArgs e)
		{
            //Nouveau conteneur
            try
			{
                string title = textBoxTitle.Text;
                string pass = textBoxPass.Text;
                Conteneur c = new Conteneur(title, pass);
                conteneurs.Add(c);
			}
            catch(Exception ex)
			{
                MessageBox.Show(ex.Message);
			}

            UpdateAllDataList();
        }
		private void buttonDelete_Click(object sender, EventArgs e)
		{
            if (inputDataList.SelectedIndex >= 0)
            {
                conteneurs.RemoveAt(inputDataList.SelectedIndex);
                UpdateAllDataList();
                if (conteneurs.Count > 0)
                    inputDataList.SelectedIndex = 0;
            }                   
        }
        private void buttonGenerate_Click(object sender, EventArgs e)
		{
            try
            {
                pwv.validateGenerale(textBoxFilePass.Text);

                string toWrite="";
                int i = 0;
                foreach(Conteneur c in conteneurs)
				{
                    toWrite += conteneurs[i].ToString();
                    i++;
				}
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                SaveFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                SaveFileDialog1.DefaultExt = "castor";
                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    toWrite = crypto.Crypt(toWrite,textBoxNom.Text, textBoxPnom.Text,textBoxFilePass.Text);
                    System.IO.File.WriteAllText(SaveFileDialog1.FileName, toWrite);
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string docTxt = System.IO.File.ReadAllText(openFileDialog.FileName);
                    string decrypt = crypto.Decrypt(docTxt, textBoxNom2.Text, textBoxPnom2.Text, textBoxFilePass2.Text);

                    string[] allConteneurs = decrypt.Split('.');
                    foreach (string s in allConteneurs)
                    {
                        outputDataList.Items.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            outputDataList.Items.Clear();
        }      
        #endregion
    }
}
