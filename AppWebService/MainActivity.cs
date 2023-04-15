using Android.App;
using Android.Icu.Lang;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace AppWebService
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            EditText txtUserID = FindViewById<EditText>(Resource.Id.txtUserID);
            EditText txtID = FindViewById<EditText>(Resource.Id.txtID);
            EditText txtName = FindViewById<EditText>(Resource.Id.txtName);
            EditText txtDescription = FindViewById<EditText>(Resource.Id.txtDescription);
            EditText btnInsert = FindViewById<EditText>(Resource.Id.btnInsert);
            EditText btnUpdate = FindViewById<EditText>(Resource.Id.btnUpdate);
            EditText btnShow = FindViewById<EditText>(Resource.Id.btnShow);
            EditText btnDelete = FindViewById<EditText>(Resource.Id.btnDelete);

            string uriService = "https://jsonplaceholder.typicode.com/posts";

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnShow.Click += async (send, e) => 
            {
                try 
                {
                    Door door = new Door();
                    Client client = new Client();

                } 
                catch (Exception X)
                {
                    Toast.MakeText(this, "Error: " + X.Message, ToastLength.Short).Show();
                }
            };

        }
        /* btnShow.Click += async (send, e) =>
         {
             try
             {
                 Door door = new Door ();
                 Client client = new Client ();

                 if (string.IsNullOrEmpty(txtID.Text))
                 {
                     int id = 0;
                     if (int.TryParse(txtID.Text.Trim(),out id))
                     {
                         var resul = await client.Get<Door>(uriService + "/" + txtID.Text);
                         if (client.codeHTTP == 200)
                         {
                             txtName.Text = resul.Title;
                             txtDescription.Text = resul.Body;
                             Toast.MakeText(this, "Consular terminada", ToastLength.Short).Show();
                         }
                     }
                 }
             }
           
         };
     }
        */

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

public class Door
{
    public Door()
    {
        UserId = 1;
        ID = 0;
        Title = "";
        Body = "";
    }

    public int UserId { get; set; }
    public int ID { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
    public class Client
    {
        public Client() 
        {
            codeHTTP = 200;
        }
        public int codeHTTP { get; set; }

        public async Task<T>Get<T>(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            codeHTTP = (int)response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T>Post<T>(Door item,  string url)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "aplication/Json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(url, content);
            json = await response.Content.ReadAsStringAsync();
            codeHTTP = (int)response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
