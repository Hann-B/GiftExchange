using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TIY_GiftExchange.Models;

namespace TIY_GiftExchange.Services
{
    public class GiftServices
    {
        const string connectionString = @"Server=localhost\SQLEXPRESS;Database=GiftExchange;Trusted_Connection=True;";

        public static List<Gift> GetAllGifts()
        {
            var rv = new List<Gift>();
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Gift";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new Gift(reader));
                }
                connection.Close();
            }
            return rv;
        }

        public Gift CreateGift(Gift Gift)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"INSERT INTO [dbo].[Gift]
                           ([Contents]
                           ,[GiftHint]
                           ,[ColorWrappingPaper]
                           ,[Height]
                           ,[Width]
                           ,[Depth]
                           ,[Weight]
                           ,[IsOpened])                       
                            VALUES ( @Contents, @GiftHint, @ColorWrappingPaper, @Height, @Width, @Depth, @Weight, @IsOpened)";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Contents", Gift.Contents);
                cmd.Parameters.AddWithValue("@GiftHint", Gift.GiftHint);
                cmd.Parameters.AddWithValue("@ColorWrappingPaper", Gift.ColorWrappingPaper);
                cmd.Parameters.AddWithValue("@Height", Gift.Height);
                cmd.Parameters.AddWithValue("@Width", Gift.Width);
                cmd.Parameters.AddWithValue("@Depth", Gift.Depth);
                cmd.Parameters.AddWithValue("@Weight", Gift.Weight);
                cmd.Parameters.AddWithValue("@IsOpened", Gift.IsOpened);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Gift;
        }

        public Gift EditGift(Gift Gift)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"UPDATE [dbo].[Gift]
                           SET[Contents]=@Contents
                           ,[GiftHint]=@GiftHint
                           ,[ColorWrappingPaper]=@ColorWrappingPaper
                           ,[Height]=@Height
                           ,[Width]=@Width
                           ,[Depth]=@Depth
                           ,[Weight]=@Weight
                           ,[IsOpened]=@IsOpened";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Contents", Gift.Contents);
                cmd.Parameters.AddWithValue("@GiftHint", Gift.GiftHint);
                cmd.Parameters.AddWithValue("@ColorWrappingPaper", Gift.ColorWrappingPaper);
                cmd.Parameters.AddWithValue("@Height", Gift.Height);
                cmd.Parameters.AddWithValue("@Width", Gift.Width);
                cmd.Parameters.AddWithValue("@Depth", Gift.Depth);
                cmd.Parameters.AddWithValue("@Weight", Gift.Weight);
                cmd.Parameters.AddWithValue("@IsOpened", Gift.IsOpened);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Gift;
        }
        public void UpdateModelService(FormCollection collection)
        {
            var gifts = new Gift
            {
                Contents = collection["Contents"],
                GiftHint = collection["GiftHint"],
                ColorWrappingPaper = collection["ColorWrappingPaper"],
                Height = double.Parse(collection["Height"]),
                Depth = double.Parse(collection["Depth"]),
                Width = double.Parse(collection["Width"]),
                Weight = double.Parse(collection["Weight"]),
                IsOpened = bool.Parse(collection["IsOpened"])
            };
        }
    }
}