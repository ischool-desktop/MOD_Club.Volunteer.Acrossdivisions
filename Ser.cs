using FISCA.DSAClient;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MOD_Club_Acrossdivisions
{
    static public class Ser
    {
        /// <summary>
        /// 測試這個學校是否可以連限
        /// 如果可以回傳 "空字串"
        /// 如果失敗回傳 "錯誤訊息"
        /// (本功能會將錯誤訊息回傳至 johnny5)
        /// </summary>
        static public string CheckAccount(string AccessPoint)
        {
            Exception ex = TestConnection(AccessPoint);
            string message = "";
            if (ex != null)
            {
                SmartSchool.ErrorReporting.ReportingService.ReportException(ex);

                if (ex.Message == "Contract Not found:ClubAcrossdivisions")
                {
                    message = "失敗：Service不存在";
                }
                else if (ex.Message.Contains("User doesn't exist"))
                {
                    message = "失敗：帳號不存在";
                }
                else if (ex.Message.Contains("解析名稱失敗"))
                {
                    message = "失敗：解析名稱失敗";
                }
                else if (ex.Message == "並未將物件參考設定為物件的執行個體。")
                {
                    message = "失敗：帳號不是ischool Account";
                }
                else
                {
                    message = "失敗：其它";
                }
            }
            else
                message = "成功";

            return message;
        }

        /// <summary>
        /// 傳入學校位置
        /// 檢查是否有登入權限
        /// </summary>
        static public Exception TestConnection(string AccessPoint)
        {
            try
            {
                Connection me = new Connection();
                me.Connect(AccessPoint, "ClubAcrossdivisions", FISCA.Authentication.DSAServices.PassportToken);
            }
            catch (Exception ex)
            {
                return ex;
            }
            return null;
        }

        /// <summary>
        /// 登入學校
        /// </summary>
        static public void Join()
        {
            //條件
            //<Request>
            //　<Field>
            //　　<All></All>
            //　</Field>
            //　<Condition>
            //　　<Uid>74115</Uid>
            //　</Condition>
            //</Request>

            //讀取已連限設定內容
            //嘗試依據使用者曾經連線內容再次連限
            //使用帳號密碼直接連線
            //Connection conection = new Connection();
            //conection.Connect("dev.sh_d", "ClubAcrossdivisions", "帳號", "密碼");

            //取得另一個學校的連線
            //Connection you = FISCA.Authentication.DSAServices.GetConnection("dev.jh_kh", "ClubAcrossdivisions");

            //取得掛載模組之學校Contract
            try
            {

                //Connection me = FISCA.Authentication.DSAServices.GetConnection(Asspoint, "ClubAcrossdivisions");
                Connection me = new Connection();
                me.Connect(tool.Point, "ClubAcrossdivisions", FISCA.Authentication.DSAServices.PassportToken);
                me = me.AsContract("ClubAcrossdivisions");

                FISCA.DSAClient.XmlHelper _xml = new XmlHelper("<Reqluest/>");
                _xml.AddElement("Field");
                _xml.AddElement("Field", "All");
                _xml.AddElement("Condition");
                _xml.AddElement("Condition", "Uid", "74115");

                Envelope rsp = me.SendRequest("_.GetClubList", new Envelope(_xml));
                MsgBox.Show(XmlHelper.Format(rsp.XmlString));

                XElement clubs = XElement.Parse(rsp.Body.XmlString);

                foreach (XElement club in clubs.Elements("K12.clubrecord.universal"))
                {


                }
            }
            catch (Exception ex)
            {


            }
        }
    }
}
