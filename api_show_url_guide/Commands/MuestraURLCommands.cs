using api_showPackageStatus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using api_show_url_guide.Models;

namespace api_show_url_guide.Commands
{
    public class obtenerURLCommands : DBContex
    {
        public  Reply muestraURL(Response oDatos)
        {
            
            Reply oReply = new Reply();
            string connectionString = $"server ={GetRDSConections().Reader}; {Data_base}";
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {

                MySqlCommand cmd = new MySqlCommand("muestra_guiaURL_sp", conexion);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@guiguia", oDatos.Guide_Number);
                cmd.Parameters.AddWithValue("@cliidentificador", oDatos.Recipient_Identifier);
                try
                {

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            oReply.URL = dr["gui_url"].ToString();
                            oReply.Message = "Ok";
                            oReply.Result = 200;
                            
                        }
                        if(oReply.URL == null)
                        {
                            oReply.URL = null;
                            oReply.Message = "No matches found";
                            oReply.Result = 401;
                        }
                    }
                   
                }
                catch (Exception ex)
                {
                    oReply.URL = null;
                    oReply.Result = 401;
                    oReply.Message = ex.Message.ToString();
                    return oReply;
                }


            }
            return oReply;

        }
    }
}