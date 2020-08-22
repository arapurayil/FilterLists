﻿using System;
using System.Collections.Generic;
using FilterLists.Directory.Infrastructure.Persistence.Queries.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilterLists.Directory.Infrastructure.Persistence.Queries.Mappings
{
    internal class DependentTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Dependent
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            const string dependencyFilterListId = nameof(Dependent.DependencyFilterList) + "Id";
            const string dependentFilterListId = nameof(Dependent.DependentFilterList) + "Id";
            builder.Property<ushort>(dependencyFilterListId);
            builder.Property<ushort>(dependentFilterListId);
            builder.HasKey(dependencyFilterListId, dependentFilterListId);
            builder.HasOne(d => d.DependencyFilterList)
                .WithMany(f => (IEnumerable<TEntity>)f.DependencyFilterLists)
                .HasForeignKey(dependencyFilterListId);
            builder.HasOne(d => d.DependentFilterList)
                .WithMany(f => (IEnumerable<TEntity>)f.DependentFilterLists)
                .HasForeignKey(dependentFilterListId);
        }
    }
}