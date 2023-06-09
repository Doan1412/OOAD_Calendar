﻿using Connector_Tier;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Tier
{
    public class BSAppointment
    {
        ConnectorFactory connector =null;
        public BSAppointment()
        {
            connector = new ConnectorFactory();
        }
        public void saveAppointment()
        {

        }
        public List<Appointment> getListAppointmentByUserId(int id)
        {
            SqlDataReader reader = null;
            List<Appointment> list = new List<Appointment>();
            try
            {
                BSUser l = new BSUser();
                reader = connector.getListById("GetAppointmentByUserId", "@Userid", id);
                while (reader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        StartTime = reader.GetDateTime(reader.GetOrdinal("startTime")),
                        EndTime = reader.GetDateTime(reader.GetOrdinal("endTime")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        Description = reader.GetString(reader.GetOrdinal("description")),
                        HostId = reader.GetInt32(reader.GetOrdinal("HostID")),
                        Location = reader.GetString(reader.GetOrdinal("location")),
                    };
                    list.Add(appointment);
                }
                reader.Close();
                connector.closeConnection();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connector.closeConnection();
            }
            return list;
        }
        public List <Appointment> getListAppointmentByIdAndDate(int id,DateTime date)
        {
            List<Appointment> list = new List<Appointment>();
            try
            {
                BSUser l = new BSUser();
                SqlDataReader reader = connector.getListAppByIdDate(id, date);
                while (reader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        StartTime = reader.GetDateTime(reader.GetOrdinal("startTime")),
                        EndTime = reader.GetDateTime(reader.GetOrdinal("endTime")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        Description = reader.GetString(reader.GetOrdinal("description")),
                        HostId = reader.GetInt32(reader.GetOrdinal("HostID")),
                        Location = reader.GetString(reader.GetOrdinal("location")),
                    };
                    list.Add(appointment);
                }
                connector.closeConnection();
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connector.closeConnection();
            }
            return list;
        }
        public List<Appointment> getListAppointmentByIdAndDateAndStatus(int id, DateTime date,bool status)
        {
            List<Appointment> list = new List<Appointment>();
            try
            {
                BSUser l = new BSUser();
                SqlDataReader reader = connector.getListAppByIdDateStatus(id, date,status);
                while (reader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        StartTime = reader.GetDateTime(reader.GetOrdinal("startTime")),
                        EndTime = reader.GetDateTime(reader.GetOrdinal("endTime")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        Description = reader.GetString(reader.GetOrdinal("description")),
                        HostId = reader.GetInt32(reader.GetOrdinal("HostID")),
                        Location = reader.GetString(reader.GetOrdinal("location")),
                    };
                    list.Add(appointment);
                }
                connector.closeConnection();
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connector.closeConnection();
            }
            return list;
        }
        public void addAppointment(Appointment appointment, List<int> ids)
        {
            try
            {
                var AppID = connector.addAppointment(appointment);  
                foreach (int id in ids)
                {
                    connector.UserToAppointment("InsertAppointmentUser", id, AppID);
                }
                connector.updateStatus(appointment.HostId, AppID, true);
            }
            catch ( Exception e)
            {
                throw e;
            }
            finally
            {
                connector.closeConnection();
            }
        }
        public void updateAppointment(Appointment appointment)
        {
            try
            {
                connector.updateAppointment(appointment);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connector.closeConnection();
            }
        }
        public void deleteAppointment(int id)
        {
            try
            {
                connector.getListById("deleteAppointment", "@AppointmentId", id);
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                connector.closeConnection();
            }
        }
        public List<Appointment> getListAppointmentNotAcceptedYet(int userId)
        {
            List<Appointment> list = new List<Appointment>();
            try
            {
                SqlDataReader reader = connector.getListById("getAppNotAcceptedYet", "@userId", userId);
                while (reader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        StartTime = reader.GetDateTime(reader.GetOrdinal("startTime")),
                        EndTime = reader.GetDateTime(reader.GetOrdinal("endTime")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        Description = reader.GetString(reader.GetOrdinal("description")),
                        HostId = reader.GetInt32(reader.GetOrdinal("HostID")),
                        Location = reader.GetString(reader.GetOrdinal("location")),
                    };
                    list.Add(appointment);
                }
                connector.closeConnection();
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connector.closeConnection();
            }
            return list;
        }
        public void updateStatus(int userId,int appointmentId,Boolean status)
        {
            try
            {
                connector.updateStatus(userId,appointmentId,status);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connector.closeConnection();
            }
        }
        public void deleteUserAppointment(int userId, int appointmentId)
        {
            try
            {
                connector.UserToAppointment("deleteUserAppointment", userId,appointmentId);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connector.closeConnection();
            }
        }
        public Appointment getAppointment(int id)
        {
            List<Appointment> list = new List<Appointment>();
            try
            {
                SqlDataReader reader = connector.getListById("getAppointmentById", "@appointmentId", id);
                while (reader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        StartTime = reader.GetDateTime(reader.GetOrdinal("startTime")),
                        EndTime = reader.GetDateTime(reader.GetOrdinal("endTime")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        Description = reader.GetString(reader.GetOrdinal("description")),
                        HostId = reader.GetInt32(reader.GetOrdinal("HostID")),
                        Location = reader.GetString(reader.GetOrdinal("location")),
                    };
                    list.Add(appointment);
                }
                connector.closeConnection();
                reader.Close();
                return list[0];
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
