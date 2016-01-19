
using Newtonsoft.Json;
using Structure.Control;
using System.Collections.Generic;
using System.IO;

namespace Structure
{
    public class BuilderDemo
    {
        public const string TestFile = @"c:\controlapp.knxjson";
        public static KNXApp TestCase1()
        {

            // 应用
            KNXApp app = new KNXApp();
            app.Areas = new List<KNXArea>();
            app.About = "Data Structure Test";
            app.DefaultLanguage = Language.Chinese;
            app.Symbol = "res/img/default_app.png";
            app.Logo = "res/img/default_app.png";
            app.Text = "ayControl Demo";



            // 楼层
            KNXArea groundFloor = new KNXArea();
            groundFloor.Rooms = new List<KNXRoom>();
            KNXArea firstFloor = new KNXArea();
            firstFloor.Rooms = new List<KNXRoom>();
            KNXArea secondFloor = new KNXArea();
            secondFloor.Rooms = new List<KNXRoom>();
            KNXArea general = new KNXArea();
            general.Rooms = new List<KNXRoom>();

            app.Areas.Add(groundFloor);
            app.Areas.Add(firstFloor);
            app.Areas.Add(secondFloor);
            app.Areas.Add(general);

            groundFloor.Text = "Ground Floor";


            firstFloor.Text = "First Floor";


            secondFloor.Text = "Second Floor";


            general.Text = "General";


            // 底层
            KNXRoom home = new KNXRoom();
            home.Pages = new List<KNXPage>();
            KNXRoom entrance = new KNXRoom();
            entrance.Pages = new List<KNXPage>();
            KNXRoom outdoor = new KNXRoom();
            outdoor.Pages = new List<KNXPage>();

            groundFloor.Rooms.Add(home);
            groundFloor.Rooms.Add(entrance);
            groundFloor.Rooms.Add(outdoor);


            home.Text = "Home";
            home.Symbol = "res/img/default_app.png";


            entrance.Text = "Entrance";
            entrance.Symbol = "res/img/default_app.png";


            outdoor.Text = "Outdoor";
            outdoor.Symbol = "res/img/default_app.png";


            // 一层
            KNXRoom livingRoom = new KNXRoom();
            KNXRoom bedRoom = new KNXRoom();

            firstFloor.Rooms.Add(livingRoom);
            firstFloor.Rooms.Add(bedRoom);

            livingRoom.Text = "Living Room";
            livingRoom.Symbol = "res/img/default_app.png";


            bedRoom.Text = "Bedroom";
            bedRoom.Symbol = "res/img/default_app.png";

            // 二层
            KNXRoom bedRoom2 = new KNXRoom();

            secondFloor.Rooms.Add(bedRoom2);


            bedRoom2.Text = "Bedroom";
            bedRoom2.Symbol = "res/img/default_app.png";

            // 其他
            KNXRoom energy = new KNXRoom();
            KNXRoom securityPin = new KNXRoom();
            KNXRoom climate = new KNXRoom();


            general.Rooms.Add(energy);
            general.Rooms.Add(securityPin);
            general.Rooms.Add(climate);

            energy.Text = "Energy";
            energy.Symbol = "res/img/default_app.png";

            securityPin.Text = "Security Pin123";
            securityPin.Symbol = "res/img/default_app.png";

            climate.Text = "Climate";
            climate.Symbol = "res/img/default_app.png";

            // home
            KNXPage homePage = new KNXPage();
            homePage.Grids = new List<KNXGrid>();
            home.Pages.Add(homePage);
            homePage.Text = "Page";
            homePage.BackgroudImage = "res/image/backup_image.png";
            homePage.ColumnCount = 1;
            homePage.RowCount = 9;


            KNXGrid top = new KNXGrid();
            top.Controls = new List<KNXControlBase>();
            KNXGrid bottom = new KNXGrid();
            bottom.Controls = new List<KNXControlBase>();

            homePage.Grids.Add(top);
            homePage.Grids.Add(bottom);

            top.Column = 0;
            top.Row = 0;
            top.ColumnSpan = 1;
            top.RowSpan = 4;


            bottom.Column = 0;
            bottom.Row = 5;
            bottom.ColumnSpan = 1;
            bottom.RowSpan = 4;

            KNXButton comingHome = new KNXButton();
            KNXButton leavingTheHouse = new KNXButton();

            top.Controls.Add(comingHome);
            top.Controls.Add(leavingTheHouse);

            comingHome.Text = "Coming Home";
            comingHome.Column = 0;
            comingHome.Row = 0;


            leavingTheHouse.Text = "Leaving teh House";
            leavingTheHouse.Column = 0;
            leavingTheHouse.Row = 1;


            KNXButton goingToSleep = new KNXButton();
            KNXButton gettingUp = new KNXButton();

            bottom.Controls.Add(goingToSleep);
            bottom.Controls.Add(gettingUp);


            goingToSleep.Text = "Going to Sleep";
            goingToSleep.Column = 0;
            goingToSleep.Row = 0;



            gettingUp.Text = "Getting Up";
            gettingUp.Column = 0;
            gettingUp.Row = 1;

            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;


            string json = JsonConvert.SerializeObject(app, Formatting.Indented, settings);

            json.Replace("$Type", "@class");
            File.WriteAllText(TestFile, json);

            return app;
        }

        public static KNXApp TestCase2()
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            string json = File.ReadAllText(TestFile);
            KNXApp app = JsonConvert.DeserializeObject<KNXApp>(json, settings);

            KNXArea a = app.Areas[0];
            KNXRoom b = a.Rooms[0];
            KNXPage c = b.Pages[0];
            KNXGrid d = c.Grids[0];
            var e = d.Controls[0];

            return app;
        }
    }
}

