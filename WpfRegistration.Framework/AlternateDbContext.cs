using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using WpfRegistration.Domain.Models;

namespace WpfRegistration.EntityFramework
{
    public class AlternateDbContext
    {
        public static ObservableCollection<UserModel> GetUsers()
        {
            string query = @"SELECT * FROM [WpfTestDb].[dbo].[Users]";
            List<UserModel> result = new List<UserModel>();
            SqlConnection con = new SqlConnection(DbConnection.cs);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                UserModel user = new UserModel();
                user.Id = Guid.Parse(sdr["Id"].ToString());
                user.FirstName = sdr["FirstName"].ToString();
                user.LastName = sdr["LastName"].ToString();
                user.Address = sdr["Address"].ToString();
                user.VaccineName = sdr["VaccineName"].ToString();
                result.Add(user);
            }
            var oc = new ObservableCollection<UserModel>();
            result.ForEach(x => oc.Add(x));
            return oc;
        }

        public static ObservableCollection<UserModel> GetUsersById()
        {
            string query = @"SELECT * FROM [WpfTestDb].[dbo].[Users] where Id = '"+IdHandlers.Id+"'";
            List<UserModel> result = new List<UserModel>();
            SqlConnection con = new SqlConnection(DbConnection.cs);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                UserModel user = new UserModel();
                user.Id = Guid.Parse(sdr["Id"].ToString());
                user.FirstName = sdr["FirstName"].ToString();
                user.LastName = sdr["LastName"].ToString();
                user.Address = sdr["Address"].ToString();
                user.VaccineName = sdr["VaccineName"].ToString();
                user.Email = sdr["Email"].ToString();
                user.Username = sdr["Username"].ToString();
                user.DateFirstDose = Convert.ToDateTime(sdr["DateFirstDose"]);
                user.NumberofShots = Convert.ToInt32(sdr["NumberofShots"]);
                user.VaccineName = sdr["VaccineName"].ToString();
                user.isBoosterShot = bool.Parse(sdr["isBoosterShot"].ToString());
                user.isVaccinated = bool.Parse(sdr["isVaccinated"].ToString());
                result.Add(user);
            }
            var oc = new ObservableCollection<UserModel>();
            result.ForEach(x => oc.Add(x));
            return oc;
        }

        public static ObservableCollection<UserModel> GetUsersBySearch()
        {
            string query = @"SELECT * FROM [WpfTestDb].[dbo].[Users] where LastName LIKE  '" + SearchHandler.Search + "%'";
            List<UserModel> result = new List<UserModel>();
            SqlConnection con = new SqlConnection(DbConnection.cs);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                UserModel user = new UserModel();
                user.Id = Guid.Parse(sdr["Id"].ToString());
                user.FirstName = sdr["FirstName"].ToString();
                user.LastName = sdr["LastName"].ToString();
                user.Address = sdr["Address"].ToString();
                user.VaccineName = sdr["VaccineName"].ToString();
                user.Email = sdr["Email"].ToString();
                user.Username = sdr["Username"].ToString();
                user.DateFirstDose = Convert.ToDateTime(sdr["DateFirstDose"]);
                user.NumberofShots = Convert.ToInt32(sdr["NumberofShots"]);
                user.VaccineName = sdr["VaccineName"].ToString();
                user.isBoosterShot = bool.Parse(sdr["isBoosterShot"].ToString());
                user.isVaccinated = bool.Parse(sdr["isVaccinated"].ToString());
                result.Add(user);
            }
            var oc = new ObservableCollection<UserModel>();
            result.ForEach(x => oc.Add(x));
            return oc;
        }
    }
}







