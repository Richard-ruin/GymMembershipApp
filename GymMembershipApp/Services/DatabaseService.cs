using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GymMembershipApp.Models;
using GymMembershipApp.Helpers;

namespace GymMembershipApp.Services
{
    public class DatabaseService
    {
        private string _connectionString;

        public DatabaseService()
        {
            // Get connection string from our helper
            _connectionString = ConfigHelper.GetConnectionString("GymMemberDB");
        }

        // Get all members
        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Members ORDER BY MemberID", connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Member member = new Member
                        {
                            MemberID = (int)reader["MemberID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Address = reader["Address"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            MembershipType = reader["MembershipType"].ToString(),
                            JoinDate = (DateTime)reader["JoinDate"],
                            ExpiryDate = (DateTime)reader["ExpiryDate"],
                            ActiveStatus = (bool)reader["ActiveStatus"]
                        };

                        members.Add(member);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving members: " + ex.Message);
                }
            }

            return members;
        }

        // Get member by ID
        public Member GetMemberById(int id)
        {
            Member member = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Members WHERE MemberID = @MemberID", connection);
                command.Parameters.AddWithValue("@MemberID", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        member = new Member
                        {
                            MemberID = (int)reader["MemberID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Address = reader["Address"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            MembershipType = reader["MembershipType"].ToString(),
                            JoinDate = (DateTime)reader["JoinDate"],
                            ExpiryDate = (DateTime)reader["ExpiryDate"],
                            ActiveStatus = (bool)reader["ActiveStatus"]
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving member: " + ex.Message);
                }
            }

            return member;
        }

        // Add new member
        public bool AddMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    INSERT INTO Members (FirstName, LastName, Email, PhoneNumber, Address, 
                                         DateOfBirth, MembershipType, JoinDate, ExpiryDate, ActiveStatus)
                    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, 
                            @DateOfBirth, @MembershipType, @JoinDate, @ExpiryDate, @ActiveStatus);
                    
                    SELECT SCOPE_IDENTITY();", connection);

                command.Parameters.AddWithValue("@FirstName", member.FirstName);
                command.Parameters.AddWithValue("@LastName", member.LastName);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Address", member.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DateOfBirth", member.DateOfBirth);
                command.Parameters.AddWithValue("@MembershipType", member.MembershipType);
                command.Parameters.AddWithValue("@JoinDate", member.JoinDate);
                command.Parameters.AddWithValue("@ExpiryDate", member.ExpiryDate);
                command.Parameters.AddWithValue("@ActiveStatus", member.ActiveStatus);

                try
                {
                    connection.Open();
                    decimal newId = (decimal)command.ExecuteScalar();
                    member.MemberID = (int)newId;
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding member: " + ex.Message);
                    return false;
                }
            }
        }

        // Update existing member
        public bool UpdateMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(@"
                    UPDATE Members
                    SET FirstName = @FirstName,
                        LastName = @LastName,
                        Email = @Email,
                        PhoneNumber = @PhoneNumber,
                        Address = @Address,
                        DateOfBirth = @DateOfBirth,
                        MembershipType = @MembershipType,
                        JoinDate = @JoinDate,
                        ExpiryDate = @ExpiryDate,
                        ActiveStatus = @ActiveStatus
                    WHERE MemberID = @MemberID", connection);

                command.Parameters.AddWithValue("@MemberID", member.MemberID);
                command.Parameters.AddWithValue("@FirstName", member.FirstName);
                command.Parameters.AddWithValue("@LastName", member.LastName);
                command.Parameters.AddWithValue("@Email", member.Email);
                command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Address", member.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DateOfBirth", member.DateOfBirth);
                command.Parameters.AddWithValue("@MembershipType", member.MembershipType);
                command.Parameters.AddWithValue("@JoinDate", member.JoinDate);
                command.Parameters.AddWithValue("@ExpiryDate", member.ExpiryDate);
                command.Parameters.AddWithValue("@ActiveStatus", member.ActiveStatus);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating member: " + ex.Message);
                    return false;
                }
            }
        }

        // Delete member
        public bool DeleteMember(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Members WHERE MemberID = @MemberID", connection);
                command.Parameters.AddWithValue("@MemberID", id);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting member: " + ex.Message);
                    return false;
                }
            }
        }
    }
}