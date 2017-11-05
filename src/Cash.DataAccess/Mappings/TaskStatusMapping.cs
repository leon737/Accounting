﻿using System.Data.Entity.ModelConfiguration;
using Cash.Domain.Models;

namespace Cash.DataAccess.Mappings
{
    public class TaskStatusMapping : EntityTypeConfiguration<TaskStatus>
    {
        public TaskStatusMapping()
        {
            ToTable(nameof(TaskStatus));
            HasKey(v => v.Id);
        }
    }
}