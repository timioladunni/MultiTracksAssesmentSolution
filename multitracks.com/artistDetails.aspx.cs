using DataAccess;
using System;
using System.Reflection;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class artistDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
       
    }
    public string Artist { get; set; }
    protected void Submit(object sender, EventArgs e)
    {
        

        try
        {
            string name = Request.Form["Search"];
            int value = Convert.ToInt32(name);
            //Artist Info
            var sql = new SQL();
            sql.Parameters.Add("@artistID", value);
            var data = sql.ExecuteStoredProcedureDataReader("GetArtistDetails");
            while (data.Read())
            {
               var title = data["title"].ToString();
                var biography = data["biography"].ToString();
                var imageURL = data["imageURL"].ToString();
                var heroURL = data["heroURL"].ToString();
                HeaderImage.ImageUrl = heroURL;
                CoverImage.ImageUrl = imageURL;
                ArtistName.Text = title;
                Biography.Text = biography;
                Title = title;
            }
            
            //Album Info
            var sql2 = new SQL();
            sql2.Parameters.Add("@artistID", value);
            var data2 = sql2.ExecuteStoredProcedureDS("AlbumDetails");
            List<int> albumID = new List<int>();
            List<string> albumImages = new List<string>();
            List<string> albumName = new List<string>();
            if (data2.Tables.Count > 0)
            {
                for (int i = 0; i < data2.Tables[0].Rows.Count; i++)
                {
                    albumID.Add(data2.Tables[0].Rows[i].Field<int>("albumID"));
                    albumImages.Add(data2.Tables[0].Rows[i].Field<string>("imageURL"));
                    albumName.Add(data2.Tables[0].Rows[i].Field<string>("title"));

                }
                for (int j = 0; j < albumImages.Count; j++)
                {
                    HtmlGenericControl div = new HtmlGenericControl();
                    div.TagName = "div";
                    div.Attributes["class"] = "media-item";
                    HtmlGenericControl atag1 = new HtmlGenericControl();
                    atag1.TagName = "a";
                    atag1.Attributes["class"] = "media-item--img--link";
                    atag1.Attributes["href"] = "#";
                    atag1.Attributes["tabindex"] = "0";
                    HtmlGenericControl img = new HtmlGenericControl();
                    img.TagName = "img";
                    img.Attributes["class"] = "media-item--img";
                    img.Attributes["alt"] = albumName[j];
                    img.Attributes["src"] = albumImages[j];
                    img.Attributes["srcset"] = albumImages[j];
                    HtmlGenericControl span = new HtmlGenericControl();
                    span.TagName = "span";
                    span.InnerText = "Master";
                    span.Attributes["class"] = "image-tag";
                    atag1.Controls.Add(img);
                    atag1.Controls.Add(span);
                    HtmlGenericControl atag2 = new HtmlGenericControl();
                    atag2.TagName = "a";
                    atag2.Attributes["class"] = "media-item--title";
                    atag2.Attributes["href"] = "#";
                    atag2.Attributes["tabindex"] = "0";
                    atag2.InnerText = albumName[j];
                    HtmlGenericControl atag3 = new HtmlGenericControl();
                    atag3.TagName = "a";
                    atag3.Attributes["class"] = "media-item--subtitle";
                    atag3.Attributes["href"] = "#";
                    atag3.Attributes["tabindex"] = "0";
                    atag3.InnerText = Title;
                    div.Controls.Add(atag1);
                    div.Controls.Add(atag2);
                    div.Controls.Add(atag3);
                    albumDiv.Controls.Add(div);
                }
               
            }
            //Song Info
            var sql3 = new SQL();
            sql3.Parameters.Add("@artistID", value);
            var data3 = sql3.ExecuteStoredProcedureDS("SongDetails");
            List<int> songAlbumID = new List<int>();
            List<string> songTitle = new List<string>();
            List<decimal> songBPM = new List<decimal>();
            List<string> songImage = new List<string>();
            List<string> songAlbumName = new List<string>();
            if (data3.Tables.Count>0)
            {
                for (int k = 0; k < data3.Tables[0].Rows.Count; k++)
                {
                    songAlbumID.Add(data3.Tables[0].Rows[k].Field<int>("albumID"));
                    songTitle.Add(data3.Tables[0].Rows[k].Field<string>("title"));
                    songBPM.Add(data3.Tables[0].Rows[k].Field<decimal>("bpm"));
                }
                foreach (var a in songAlbumID)
                {
                    if (albumID.Contains(a))
                    {
                       songImage.Add(albumImages[albumID.IndexOf(a)]);
                    }
                }
                foreach (var b in songAlbumID)
                {
                    if (albumID.Contains(b))
                    {
                        songAlbumName.Add(albumName[albumID.IndexOf(b)]);
                    }
                }
                for (int l = 0; l < songAlbumID.Count; l++)
                {
                    HtmlGenericControl list = new HtmlGenericControl();
                    list.TagName = "li";
                    list.Attributes["class"] = "song-list--item media-player--row";
                    HtmlGenericControl div1 = new HtmlGenericControl();
                    div1.TagName = "div";
                    div1.Attributes["class"] = "song-list--item--player-img media-player";
                    HtmlGenericControl img1 = new HtmlGenericControl();
                    img1.TagName = "img";
                    img1.Attributes["class"] = "song-list--item--player-img--img";
                    img1.Attributes["srcset"] = songImage[l];
                    img1.Attributes["src"] = songImage[l];
                    div1.Controls.Add(img1);
                    HtmlGenericControl div2 = new HtmlGenericControl();
                    div2.TagName = "div";
                    div2.Attributes["class"] = "song-list--item--right";
                    HtmlGenericControl a1 = new HtmlGenericControl();
                    a1.TagName = "a";
                    a1.Attributes["href"] = "#";
                    a1.Attributes["class"] = "song-list--item--primary";
                    a1.InnerText = songTitle[l];
                    HtmlGenericControl a2 = new HtmlGenericControl();
                    a2.TagName = "a";
                    a2.Attributes["class"] = "song-list--item--secondary";
                    a2.InnerText = songAlbumName[l];
                    HtmlGenericControl a3 = new HtmlGenericControl();
                    a3.TagName = "a";
                    a3.Attributes["class"] = "song-list--item--secondary";
                    a3.InnerText = $"{songBPM[l].ToString()} BPM";
                    HtmlGenericControl a4 = new HtmlGenericControl();
                    a4.TagName = "a";
                    a4.Attributes["class"] = "song-list--item--secondary";
                    a4.InnerText = "4/4";
                    div2.Controls.Add(a1);
                    div2.Controls.Add(a2);
                    div2.Controls.Add(a3);
                    div2.Controls.Add(a4);
                    HtmlGenericControl div3 = new HtmlGenericControl();
                    div3.TagName = "div";
                    div3.Attributes["class"] = "song-list--item--icon--holder";
                    HtmlGenericControl a5 = new HtmlGenericControl();
                    a5.TagName = "a";
                    a5.Attributes["href"] = "#";
                    a5.Attributes["class"] = "song-list--item--icon--wrap";
                    a5.Attributes["style"] = "display: inline-block";
                    HtmlGenericControl svg1 = new HtmlGenericControl();
                    svg1.TagName = "svg";
                    svg1.Attributes["class"] = "song-list--item--icon";
                    svg1.Attributes["xmlns"] = "http://www.w3.org/2000/svg";
                    svg1.Attributes["viewBox"] = "0 0 24 24";
                    svg1.Attributes["id"] = "ds-tracks-sm";
                    HtmlGenericControl path1 = new HtmlGenericControl();
                    path1.TagName = "path";
                    path1.Attributes["fill-rule"] = "evenodd";
                    path1.Attributes["clip-rule"] = "evenodd";
                    path1.Attributes["d"] = "M1.977 12c0-5.523 4.477-10 10-10s10 4.477 10 10-4.477 10-10 10-10-4.477-10-10zm4.04 6.204a8.579 8.579 0 005.96 2.405c4.747 0 8.61-3.862 8.61-8.609 0-4.746-3.863-8.609-8.61-8.609a8.573 8.573 0 00-5.893 2.343h6.598a.708.708 0 110 1.415H4.87c-.29.423-.543.875-.754 1.348h11.213a.708.708 0 110 1.415H3.624c-.109.437-.184.888-.223 1.35h14.637a.708.708 0 110 1.414H3.396c.037.46.109.911.215 1.348h13.025a.708.708 0 010 1.416H4.087c.207.473.454.923.739 1.349h9.164a.708.708 0 110 1.415H6.017z";
                    path1.Attributes["fill"] = "";
                    svg1.Controls.Add(path1);
                    a5.Controls.Add(svg1);

                    HtmlGenericControl a6 = new HtmlGenericControl();
                    a6.TagName = "a";
                    a6.Attributes["href"] = "#";
                    a6.Attributes["class"] = "song-list--item--icon--wrap";
                    a6.Attributes["style"] = "display: inline-block";
                    HtmlGenericControl svg2 = new HtmlGenericControl();
                    svg2.TagName = "svg";
                    svg2.Attributes["class"] = "song-list--item--icon";
                    svg2.Attributes["xmlns"] = "http://www.w3.org/2000/svg";
                    svg2.Attributes["viewBox"] = "0 0 24 24";
                    svg2.Attributes["id"] = "ds-custommix-sm";
                    HtmlGenericControl path2 = new HtmlGenericControl();
                    path2.TagName = "path";
                    path2.Attributes["fill-rule"] = "evenodd";
                    path2.Attributes["clip-rule"] = "evenodd";
                    path2.Attributes["d"] = "M3.887 2h16.226C21.155 2 22 2.845 22 3.887v16.226A1.887 1.887 0 0120.113 22H3.887A1.887 1.887 0 012 20.113V3.887C2 2.845 2.845 2 3.887 2zm15.681 18.62c.581 0 1.052-.47 1.052-1.051V4.431c0-.58-.47-1.051-1.052-1.051H4.432c-.581 0-1.052.47-1.052 1.051v15.138c0 .58.47 1.051 1.052 1.051h15.136zM6.46 12v6.233a.692.692 0 101.385 0V12c.764 0 1.385-.621 1.385-1.385V9.23c0-.763-.621-1.385-1.385-1.385V5.767a.692.692 0 10-1.385 0v2.078c-.764 0-1.385.622-1.385 1.385v1.385c0 .764.621 1.385 1.385 1.385zm1.385-1.385H6.46V9.23h1.385v1.385zm3.463 5.54v2.078a.692.692 0 101.384 0v-2.078c.764 0 1.386-.622 1.386-1.385v-1.385c0-.764-.622-1.385-1.386-1.385V5.767a.692.692 0 10-1.384 0V12c-.764 0-1.386.621-1.386 1.385v1.385c0 .763.622 1.385 1.386 1.385zm1.384-1.385h-1.384v-1.385h1.384v1.385zM16.155 12v6.233a.692.692 0 101.385 0V12c.764 0 1.385-.621 1.385-1.385V9.23c0-.763-.621-1.385-1.385-1.385V5.767a.692.692 0 10-1.385 0v2.078c-.764 0-1.385.622-1.385 1.385v1.385c0 .764.621 1.385 1.385 1.385zm1.385-1.385h-1.385V9.23h1.385v1.385z";
                    path2.Attributes["fill"] = "";
                    svg2.Controls.Add(path2);
                    a6.Controls.Add(svg2);


                    HtmlGenericControl a7 = new HtmlGenericControl();
                    a7.TagName = "a";
                    a7.Attributes["href"] = "#";
                    a7.Attributes["class"] = "song-list--item--icon--wrap";
                    a7.Attributes["style"] = "display: inline-block";
                    HtmlGenericControl svg3 = new HtmlGenericControl();
                    svg3.TagName = "svg";
                    svg3.Attributes["class"] = "song-list--item--icon";
                    svg3.Attributes["xmlns"] = "http://www.w3.org/2000/svg";
                    svg3.Attributes["viewBox"] = "0 0 24 24";
                    svg3.Attributes["id"] = "ds-rehearsalmix-sm";
                    HtmlGenericControl path3 = new HtmlGenericControl();
                    path3.TagName = "path";
                    path3.Attributes["d"] = "M5.892 18.468h2.155a.719.719 0 100-1.438H5.892a.718.718 0 000 1.438zM8.047 15.593H5.892a.718.718 0 010-1.438h2.155a.719.719 0 110 1.438zM10.922 18.468h2.156a.719.719 0 100-1.438h-2.156a.718.718 0 100 1.438zM13.078 15.593h-2.156a.718.718 0 110-1.438h2.156a.719.719 0 110 1.438zM10.922 12.719h2.156a.719.719 0 100-1.438h-2.156a.718.718 0 100 1.438zM13.078 9.844h-2.156a.718.718 0 110-1.437h2.156a.719.719 0 110 1.437zM5.892 12.719h2.155a.719.719 0 100-1.438H5.892a.718.718 0 000 1.438zM18.108 18.468h-2.156a.718.718 0 110-1.438h2.156a.719.719 0 110 1.438zM15.952 15.593h2.156a.719.719 0 100-1.438h-2.156a.718.718 0 100 1.438zM18.108 12.719h-2.156a.718.718 0 110-1.438h2.156a.719.719 0 110 1.438zM15.952 9.844h2.156a.719.719 0 100-1.437h-2.156a.718.718 0 100 1.437zM13.078 6.97h-2.156a.718.718 0 110-1.438h2.156a.719.719 0 110 1.437z";
                    path3.Attributes["fill"] = "";
                    HtmlGenericControl path31 = new HtmlGenericControl();
                    path31.TagName = "path";
                    path31.Attributes["d"] = "M3.887 2h16.226C21.155 2 22 2.845 22 3.887v16.226A1.887 1.887 0 0120.113 22H3.887A1.887 1.887 0 012 20.113V3.887C2 2.845 2.845 2 3.887 2zm15.681 18.62c.581 0 1.052-.47 1.052-1.051V4.431c0-.58-.47-1.051-1.052-1.051H4.432c-.581 0-1.052.47-1.052 1.051v15.138c0 .58.47 1.051 1.052 1.051h15.136z";
                    path31.Attributes["fill"] = "";
                    svg3.Controls.Add(path3);
                    svg3.Controls.Add(path31);
                    a7.Controls.Add(svg3);



                    HtmlGenericControl a8 = new HtmlGenericControl();
                    a8.TagName = "a";
                    a8.Attributes["href"] = "#";
                    a8.Attributes["class"] = "song-list--item--icon--wrap";
                    a8.Attributes["style"] = "display: inline-block";
                    HtmlGenericControl svg4 = new HtmlGenericControl();
                    svg4.TagName = "svg";
                    svg4.Attributes["class"] = "song-list--item--icon";
                    svg4.Attributes["xmlns"] = "http://www.w3.org/2000/svg";
                    svg4.Attributes["viewBox"] = "0 0 24 24";
                    svg4.Attributes["id"] = "ds-sounds-sm";
                    HtmlGenericControl path4 = new HtmlGenericControl();
                    path4.TagName = "path";
                    path4.Attributes["fill-rule"] = "evenodd";
                    path4.Attributes["clip-rule"] = "evenodd";
                    path4.Attributes["d"] = "M13.36 19.152h-.004a.606.606 0 01-.597-.528l-1.175-9.038-.964 5.787a.607.607 0 01-.574.506.625.625 0 01-.612-.459l-1.24-4.962-.9 1.787a.632.632 0 01-.553.341H5.79a.62.62 0 01-.627-.594.606.606 0 01.606-.618h.374c.143 0 .273-.08.337-.207l1.364-2.712a.606.606 0 011.13.125l.93 3.719 1.157-6.945a.606.606 0 01.598-.506h.01c.3.005.552.23.59.528l1.132 8.705 1.02-7.077a.606.606 0 01.586-.52h.014c.29 0 .54.207.595.493l1.22 6.414.836-1.68a.606.606 0 01.543-.337h.96a.62.62 0 01.626.595.606.606 0 01-.607.617h-.37a.377.377 0 00-.338.21l-1.366 2.747a.605.605 0 01-1.138-.157l-.879-4.619-1.133 7.865a.606.606 0 01-.6.52z"; 
                    path4.Attributes["fill"] = "";
                    HtmlGenericControl path41 = new HtmlGenericControl();
                    path41.TagName = "path";
                    path41.Attributes["fill-rule"] = "evenodd";
                    path41.Attributes["clip-rule"] = "evenodd";
                    path41.Attributes["d"] = "M12.477 2c-5.523 0-10 4.477-10 10s4.477 10 10 10 10-4.477 10-10S18 2 12.477 2zm0 1.312c4.79 0 8.688 3.898 8.688 8.688 0 4.79-3.898 8.688-8.688 8.688-4.79 0-8.688-3.898-8.688-8.688 0-4.79 3.898-8.688 8.688-8.688z";
                    path41.Attributes["fill"] = "";
                    svg4.Controls.Add(path4);
                    svg4.Controls.Add(path41);
                    a8.Controls.Add(svg4);




                    HtmlGenericControl a9 = new HtmlGenericControl();
                    a9.TagName = "a";
                    a9.Attributes["href"] = "#";
                    a9.Attributes["class"] = "song-list--item--icon--wrap";
                    a9.Attributes["style"] = "display: inline-block";
                    HtmlGenericControl svg5 = new HtmlGenericControl();
                    svg5.TagName = "svg";
                    svg5.Attributes["class"] = "song-list--item--icon";
                    svg5.Attributes["xmlns"] = "http://www.w3.org/2000/svg";
                    svg5.Attributes["viewBox"] = "0 0 24 24";
                    svg5.Attributes["id"] = "ds-charts-sm";
                    HtmlGenericControl path5 = new HtmlGenericControl();
                    path5.TagName = "path";
                    path5.Attributes["d"] = "M7.352 9.077h8.486a.65.65 0 01.653.65.65.65 0 01-.653.65H7.352a.65.65 0 110-1.3zM7.352 7.778h4.57a.65.65 0 100-1.299h-4.57a.65.65 0 00-.653.65c0 .359.292.65.653.65zM7.352 11.675h6.136a.65.65 0 01.653.65.65.65 0 01-.653.65H7.352a.65.65 0 01-.652-.65c0-.36.292-.65.652-.65zM11.008 14.273H7.352a.65.65 0 00-.653.65c0 .359.292.65.653.65h3.656a.65.65 0 00.653-.65c0-.36-.293-.65-.653-.65zM7.352 16.871h3.656c.36 0 .653.29.653.65a.65.65 0 01-.653.65H7.352a.65.65 0 01-.653-.65c0-.36.292-.65.653-.65zM17.334 12.856c.35.742.127.912-.236.912-.128 0-.2-.096-.283-.21-.118-.159-.26-.351-.62-.352h-.002v3.86c0 .687-.746 1.243-1.666 1.243-.92 0-1.666-.556-1.666-1.243s.745-1.244 1.666-1.244h.833v-3.73c0-.23.186-.415.417-.415.01-.001.962-.085 1.557 1.178z"; 
                    path5.Attributes["fill"] = "";
                    HtmlGenericControl path51 = new HtmlGenericControl();
                    path51.TagName = "path";
                    path51.Attributes["fill-rule"] = "evenodd";
                    path51.Attributes["clip-rule"] = "evenodd";
                    path51.Attributes["d"] = "M20.078 5.903L16.47 2.267A.9.9 0 0015.831 2H4.853C4.201 2 3.5 2.474 3.5 3.515v16.97C3.5 21.32 4.174 22 5.004 22h13.835c.83 0 1.504-.68 1.504-1.515V6.545a.912.912 0 00-.265-.642zm-.977 14.865H4.741V3.232h9.791v3.572c0 .538.439.975.98.975h3.59v12.989zm-.015-14.221h-3.312V3.243l3.312 3.304z";
                    path51.Attributes["fill"] = "";
                    svg5.Controls.Add(path5);
                    svg5.Controls.Add(path51);
                    a9.Controls.Add(svg5);



                    HtmlGenericControl a10 = new HtmlGenericControl();
                    a10.TagName = "a";
                    a10.Attributes["href"] = "#";
                    a10.Attributes["class"] = "song-list--item--icon--wrap";
                    a10.Attributes["style"] = "display: inline-block";
                    HtmlGenericControl svg6 = new HtmlGenericControl();
                    svg6.TagName = "svg";
                    svg6.Attributes["class"] = "song-list--item--icon";
                    svg6.Attributes["xmlns"] = "http://www.w3.org/2000/svg";
                    svg6.Attributes["viewBox"] = "0 0 24 24";
                    svg6.Attributes["id"] = "ds-propresenter-sm";
                    HtmlGenericControl path6 = new HtmlGenericControl();
                    path6.TagName = "path";
                    path6.Attributes["fill-rule"] = "evenodd";
                    path6.Attributes["clip-rule"] = "evenodd";
                    path6.Attributes["d"] = "M23 5.625c0 .345-.28.625-.625.625H21V18a2 2 0 01-2 2H5a2 2 0 01-2-2V6.25H1.625a.625.625 0 110-1.25h20.75c.345 0 .625.28.625.625zM4.25 6.25h15.5V18a.75.75 0 01-.75.75H5a.75.75 0 01-.75-.75V6.25zm3.375 2.5a.625.625 0 100 1.25h8.75a.625.625 0 100-1.25h-8.75zM6 12.375c0-.345.28-.625.625-.625h10.75a.625.625 0 110 1.25H6.625A.625.625 0 016 12.375zm1.625 2.375a.625.625 0 100 1.25h8.75a.625.625 0 100-1.25h-8.75z";
                    path6.Attributes["fill"] = "";
                    svg6.Controls.Add(path6);
                    a10.Controls.Add(svg6);


                    div3.Controls.Add(a5);
                    div3.Controls.Add(a6);
                    div3.Controls.Add(a7);
                    div3.Controls.Add(a8);
                    div3.Controls.Add(a9);
                    div3.Controls.Add(a10);
                    div2.Controls.Add(div3);
                    list.Controls.Add(div1);
                    list.Controls.Add(div2);
                    playlist.Controls.Add(list);

                }
            }
        }
        catch
        {
            
        }
    }
}