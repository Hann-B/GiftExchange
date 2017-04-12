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

        public List<Gift> GetAllGifts()
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

        public static Gift GetGift(int id)
        {
            var gift = new List<Gift>();
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM Gift WHERE Id=@Id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    gift.Add(new Gift(reader));
                }
                connection.Close();
            }
            return gift[0];
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
                           ,[IsOpened]=@IsOpened
                            WHERE Id=@Id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", Gift.Id);
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

        public Gift DeleteGift(Gift Gift)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"DELETE FROM [dbo].[Gift]
                           WHERE Id=@id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Gift.Id);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            return Gift;
        }

        public List<Gift> UnopenedGifts()
        {
            var gifts = new List<Gift>();
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"SELECT *
                            FROM[dbo].[Gift]
                            WHERE IsOpened = @IsOpened;";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IsOpened", false);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    gifts.Add(new Gift(reader));
                }
                connection.Close();
            }
            return gifts;
        }

        public Gift OpenGift(Gift Gift)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"UPDATE [dbo].[Gift]
                           SET[IsOpened]=@IsOpened
                            WHERE Id=@id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Gift.Id);
                cmd.Parameters.AddWithValue("@IsOpened", true);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return Gift;
            }
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