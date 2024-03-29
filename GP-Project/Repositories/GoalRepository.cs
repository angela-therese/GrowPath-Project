﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowPath.Repositories;
using GrowPath.Models;
using Microsoft.Extensions.Configuration;
using GrowPath.Utils;

namespace GrowPath.Repositories
{
    public class GoalRepository : BaseRepository, IGoalRepository
    {
        public GoalRepository(IConfiguration configuration) : base(configuration) { }


        public List<Goal> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, Title, Description, StudentId, DateCreated, CategoryId
                    FROM Goal";

                    var reader = cmd.ExecuteReader();

                    var goals = new List<Goal>();
                    while (reader.Read())
                    {
                        goals.Add(new Goal()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Title = DbUtils.GetString(reader, "Title"),
                            Description = DbUtils.GetString(reader, "Description"),
                            StudentId = DbUtils.GetInt(reader, "StudentId"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId")
                        });


                    }
                    reader.Close();

                    return goals;
                }
            }
        }

        //END GET ALL


        public List<Goal> GetAllByUserId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT g.Id AS GoalId, g.Title AS GoalTitle, g.Description, g.StudentId, g.DateCreated, g.CategoryId, s.Id, s.ClassId, c.UserProfileId
      FROM Goal g 
      JOIN Student s ON g.StudentId = s.Id
      JOIN Course c ON s.ClassId = c.Id
      WHERE c.UserProfileId = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();

                    var goals = new List<Goal>();
                    while (reader.Read())
                    {
                        goals.Add(new Goal()
                        {
                            Id = DbUtils.GetInt(reader, "GoalId"),
                            Title = DbUtils.GetString(reader, "GoalTitle"),
                            Description = DbUtils.GetString(reader, "Description"),
                            StudentId = DbUtils.GetInt(reader, "StudentId"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"), 
                            CategoryId = DbUtils.GetInt(reader, "CategoryId")
                        });


                    }
                    reader.Close();

                    return goals;
                }
            }
        }


        //END GET ALL BY USER ID

        public Goal GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT g.Id AS GoalId, g.Title, g.Description, g.StudentId AS GoalStudentId, g.DateCreated, g.CategoryId, s.Id AS StudentId, s.FirstName, s.LastName, s.Email, s.ClassId, s.imageUrl
                        FROM Goal g 
                        JOIN Student s on g.StudentId = s.Id
                        WHERE  g.Id = @id
                            ";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();

                    Goal goal = null;
                    while (reader.Read())
                    {
                        if (goal == null)
                        {
                            goal = new Goal()
                            {
                                Id = DbUtils.GetInt(reader, "GoalId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "Description"),
                                StudentId = DbUtils.GetInt(reader, "GoalStudentId"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                                Student = new Student()
                                {
                                    Id = DbUtils.GetInt(reader, "StudentId"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    ClassId = DbUtils.GetInt(reader, "ClassId"),
                                    ImageUrl = DbUtils.GetString(reader, "ImageUrl")
                                }
                            }
                            ;
                        }
                    }
                    reader.Close();
                    return goal;

                     }
                 }    
            }

        //END BYID
        public List<Goal> GetGoalsByCourse(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT g.Id AS GoalId, g.Title, g.Description, g.DateCreated, g.CategoryId, gc.Id as CategoryId,  gc.Name AS CategoryName, s.Id AS StudentId, c.Id AS CourseId, c.Name AS CourseName 
                FROM Goal g LEFT JOIN GoalCategory gc ON g.CategoryId = gc.Id
                JOIN Student s ON g.StudentId = s.Id
                JOIN Course c ON s.ClassId = c.Id
                WHERE c.Id = @Id
                ORDER BY g.CategoryId ASC";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    var goals = new List<Goal>();
                    while (reader.Read())
                    {
                        goals.Add(new Goal()
                        {
                            Id = DbUtils.GetInt(reader, "GoalId"),
                            Title = DbUtils.GetString(reader, "Title"),
                            Description = DbUtils.GetString(reader, "Description"),
                            StudentId = DbUtils.GetInt(reader, "StudentId"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            CategoryName = DbUtils.GetString(reader,"CategoryName"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId")
                        });


                    }
                    reader.Close();

                    return goals;
                }
            }
        }

        //END BYCOURSE
        public void Add(Goal goal)
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                        INSERT INTO Goal (Title, Description, StudentId, DateCreated, CategoryId)
                        OUTPUT INSERTED.ID
                        VALUES (@Title, @Description, @StudentId, @DateCreated,  @CategoryId )";

                        DbUtils.AddParameter(cmd, "@Title", goal.Title);
                        DbUtils.AddParameter(cmd, "@Description", goal.Description);
                        DbUtils.AddParameter(cmd, "@StudentId", goal.StudentId);
                        DbUtils.AddParameter(cmd, "@DateCreated", goal.DateCreated);
                        DbUtils.AddParameter(cmd, "@CategoryId", goal.CategoryId);

                        goal.Id = (int)cmd.ExecuteScalar();
                    }
                }
            }

        //END ADD
        public void Update(Goal goal)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Goal
                           SET Title = @Title,
                               Description = @Description,
                               StudentId = @StudentId,
                               DateCreated = @DateCreated,
                               CategoryId = @CategoryId 
                              
                               
                               
                             
                         WHERE Id = @Id";


                    DbUtils.AddParameter(cmd, "@Title", goal.Title);
                    DbUtils.AddParameter(cmd, "@Description", goal.Description);
                    DbUtils.AddParameter(cmd, "@StudentId", goal.StudentId);
                    DbUtils.AddParameter(cmd, "@DateCreated", goal.DateCreated);
                    DbUtils.AddParameter(cmd, "@CategoryId", goal.CategoryId);
                    DbUtils.AddParameter(cmd, "@Id", goal.Id);


                    cmd.ExecuteNonQuery();
                }
            }
        }

        //END UPDATE

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Goal WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //END DELETE


    }

}

