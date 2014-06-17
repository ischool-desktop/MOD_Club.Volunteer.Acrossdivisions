using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOD_Club_Acrossdivisions
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {
            RibbonBarItem InClass = FISCA.Presentation.MotherForm.RibbonBarItems["志願序社團", "跨部別"];
            InClass["連線(跨部別)"].Enable = Permissions.連線權限;
            InClass["連線(跨部別)"].Image = Properties.Resources.asymmetric_network_64;
            InClass["連線(跨部別)"].Click += delegate
            {
                NowConnection con = new NowConnection();
                con.ShowDialog();
            };

            InClass["社團志願分配(跨部別)"].Image = Properties.Resources.flip_horizontal_64;
            InClass["社團志願分配(跨部別)"].Enable = Permissions.社團志願分配權限;
            InClass["社團志願分配(跨部別)"].Click += delegate
            {
                VolunteerAssignment cva = new VolunteerAssignment();
                cva.ShowDialog();
            };

            InClass["報表"].Image = Properties.Resources.Report;
            InClass["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
            InClass["報表"]["社團點名單(跨部別)"].Enable = Permissions.社團點名單權限;
            InClass["報表"]["社團點名單(跨部別)"].Click += delegate
            {
                ClubPointList cpl = new ClubPointList();
            };

            InClass["報表"]["社團概況表(跨部別)"].Enable = Permissions.社團概況表權限;
            InClass["報表"]["社團概況表(跨部別)"].Click += delegate
            {

            };

            RibbonBarItem InStudent = FISCA.Presentation.MotherForm.RibbonBarItems["學生", "資料統計"];
            InStudent["報表"]["社團相關報表"]["英文社團證明單"].Enable = false;
            InStudent["報表"]["社團相關報表"]["英文社團證明單"].Click += delegate
            {

            };

            //是否能夠只用單一代碼,決定此模組之使用
            Catalog detail1;
            detail1 = RoleAclSource.Instance["社團"]["功能項目"];
            detail1.Add(new RibbonFeature(Permissions.連線, "連線_跨部別"));
            detail1.Add(new RibbonFeature(Permissions.社團志願分配, "社團志願分配_跨部別"));

            detail1 = RoleAclSource.Instance["社團"]["報表"];
            detail1.Add(new RibbonFeature(Permissions.社團點名單, "社團點名單_跨部別"));
            detail1.Add(new RibbonFeature(Permissions.社團概況表, "社團概況表_跨部別"));

            detail1 = RoleAclSource.Instance["學生"]["報表"];
            detail1.Add(new RibbonFeature(Permissions.英文社團證明單, "英文社團證明單"));
        }
    }
}
