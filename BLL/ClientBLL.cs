using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ClientBLL
    {

        private ClientDAL clientDAL;
        public ClientBLL()
        {
            clientDAL = new ClientDAL();
        }
        public List<ClientDTO> SearchClientsByClientName(string clientName)
        {
            try
            {
                return clientDAL.SearchClientName(clientName);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public DataTable GetClientList()
        {
            try
            {
                var listClient = clientDAL.GetClientList();
                return listClient;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int GetTotalClients()
        {
            try
            {
                return clientDAL.GetTotalClients();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ClientDTO GetClientById(string clientId)
        {
            try
            {
                return clientDAL.GetClientById(clientId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public ClientDTO InsertClient(ClientDTO clientDTO)
        {
            try
            {
                ClientDTO client = new ClientDTO
                {
                    ClientId = clientDTO.ClientId,
                    ClientName = clientDTO.ClientName,
                    ClientEmail = clientDTO.ClientEmail,
                    ClientPhone = clientDTO.ClientPhone,
                    ClientAddress = clientDTO.ClientAddress,
                };
                return clientDAL.InsertClient(client);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ClientDTO UpdateClient(ClientDTO clientDTO)
        {
            try
            {
                ClientDTO client = new ClientDTO
                {
                    ClientId = clientDTO.ClientId,
                    ClientName = clientDTO.ClientName,
                    ClientEmail = clientDTO.ClientEmail,
                    ClientPhone = clientDTO.ClientPhone,
                    ClientAddress = clientDTO.ClientAddress,
                };
                return clientDAL.UpdateClient(client);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteClient(string clientId)
        {
            try
            {
                clientDAL.DeleteClient(clientId);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi" + ex.Message);
            }
        }
    }
}
