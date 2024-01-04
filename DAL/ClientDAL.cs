using DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClientDAL
    {
        DBContext connect = new DBContext();

        public DataTable GetClientList()
        {
            SqlCommand command = new SqlCommand("Select * from clients", connect.GetConnection());
            connect.OpenConnect();
            try
            {
                SqlDataReader rd = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(rd);
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetTotalClients()
        {
            int totalClients = 0;
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Clients", connect.GetConnection());
                totalClients = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return totalClients;
        }

        public ClientDTO GetClientById(string clientId)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM clients WHERE ClientId = @ClientId", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@ClientId", clientId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        ClientDTO client = new ClientDTO
                        {
                            ClientId = reader["ClientId"].ToString(),
                            ClientName = reader["ClientName"].ToString(),
                            ClientEmail = reader["ClientEmail"].ToString(),
                            ClientPhone = reader["ClientPhone"].ToString(),
                            ClientAddress = reader["ClientAddress"].ToString(),
                        };

                        return client;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error :", ex);
            }
            finally
            {
                connect.CloseConnect();
            }
        }
        public ClientDTO GeByClientName(string clientName)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM clients WHERE ClientName = @ClientName", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@ClientName", clientName);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        ClientDTO client = new ClientDTO
                        {
                            ClientId = reader["ClientId"].ToString(),
                            ClientName = reader["ClientName"].ToString(),
                            ClientEmail = reader["ClientEmail"].ToString(),
                            ClientPhone = reader["ClientPhone"].ToString(),
                            ClientAddress = reader["ClientAddress"].ToString(),
                        };
                        return client;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving client information.", ex);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public ClientDTO InsertClient(ClientDTO clientDTO)
        {
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("insert into clients( ClientId, ClientName, ClientEmail, ClientPhone, ClientAddress) values (@ClientId, @ClientName, @ClientEmail, @ClientPhone, @ClientAddress)", connect.GetConnection());
                command.Parameters.AddWithValue("@ClientId", clientDTO.ClientId);
                command.Parameters.AddWithValue("@ClientName", clientDTO.ClientName);
                command.Parameters.AddWithValue("@ClientEmail", clientDTO.ClientEmail);
                command.Parameters.AddWithValue("@ClientPhone", clientDTO.ClientPhone);
                command.Parameters.AddWithValue("@ClientAddress", clientDTO.ClientAddress);
                int result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    return clientDTO;
                }
                else
                {
                    throw new Exception("Failed to insert client.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while inserting client." + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public List<ClientDTO> SearchClientName(string clientName)
        {
            try
            {
                connect.OpenConnect();
                using (SqlCommand command = new SqlCommand("SELECT * FROM clients WHERE ClientName LIKE @ClientName", connect.GetConnection()))
                {
                    command.Parameters.AddWithValue("@ClientName", "%" + clientName + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<ClientDTO> clients = new List<ClientDTO>();

                        while (reader.Read())
                        {
                            ClientDTO client = new ClientDTO();
                            client.ClientId = reader.GetString("ClientId");
                            client.ClientName = reader.GetString("ClientName");
                            client.ClientEmail = reader.GetString("ClientEmail");
                            client.ClientPhone = reader.GetString("ClientPhone");
                            client.ClientAddress = reader.GetString("ClientAddress");
                            clients.Add(client);
                        }

                        return clients;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred" + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public ClientDTO UpdateClient(ClientDTO clientDTO)
        {
            try
            {

                connect.OpenConnect();
                SqlCommand command = new SqlCommand("UPDATE clients SET ClientName = @ClientName, ClientEmail = @ClientEmail, ClientPhone = @ClientPhone, ClientAddress = @ClientAddress WHERE ClientId = @ClientId", connect.GetConnection());
                command.Parameters.AddWithValue("@ClientId", clientDTO.ClientId);
                command.Parameters.AddWithValue("@ClientName", clientDTO.ClientName);
                command.Parameters.AddWithValue("@ClientEmail", clientDTO.ClientEmail);
                command.Parameters.AddWithValue("@ClientPhone", clientDTO.ClientPhone);
                command.Parameters.AddWithValue("@ClientAddress", clientDTO.ClientAddress);

                int result = command.ExecuteNonQuery();

                if (result == 1)
                {
                    return clientDTO;
                }
                else
                {
                    throw new Exception("Failed to update Client.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while update Client." + ex.Message);
            }
            finally
            {
                connect.CloseConnect();
            }
        }

        public void DeleteClient(string clientId)
        {
            try
            {
                connect.OpenConnect();
                SqlCommand command = new SqlCommand("delete clients where ClientId = @ClientId", connect.GetConnection());
                command.Parameters.AddWithValue("ClientId", clientId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
