using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp; 

namespace x8pvz1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        // kliens létrehozása, url cím személy szerint változhat
        RestClient loginClient = new RestClient("http://localhost/szop/login.php");

        public void ClearInput()
        {
            usernameTB.Text = "";
            passwordTB.Text = "";
        }

        private void loginBTN_Click(object sender, EventArgs e)
        {
            RestRequest request = new RestRequest();
            request.AddParameter("username", usernameTB.Text);
            request.AddParameter("password", passwordTB.Text);
            try
            {
                RestResponse response = loginClient.Get(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show(response.StatusDescription);
                }
                else
                {
                    Response res = loginClient.Deserialize<Response>(response).Data;
                    if (res.Error != 0)
                    {
                        MessageBox.Show(res.Message);
                    }
                    else
                    {
                        User u = new User()
                        {
                            Username = usernameTB.Text,
                            Password = passwordTB.Text
                        };
                        new UsersForm(u, this).Show();
                        this.Hide();
                        ClearInput();
                    }
                }
            }
            catch (DeserializationException)
            {
                MessageBox.Show("Deserialization error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
