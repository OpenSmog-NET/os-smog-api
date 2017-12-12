﻿using Microsoft.EntityFrameworkCore;
using OS.DAL.PgSql.Model;

namespace OS.DAL.PgSql
{
    public class DeviceDbContext : DbContext
    {
        public DbSet<Device> Devices { get; internal set; }
        public DbSet<PostalAddress> PostalAddresses { get; internal set; }

        public DbSet<Vendor> Vendors { get; internal set; }

        public DbSet<VendorApiKey> VendorApiKeys { get; internal set; }

        public DeviceDbContext(DbContextOptions<DeviceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Vendor
            builder.Entity<Vendor>().ToTable("Vendors");
            builder.Entity<Vendor>().HasKey(x => x.Id);
            builder.Entity<Vendor>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Vendor>().HasMany(x => x.Keys)
                .WithOne()
                .HasForeignKey(x => x.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Vendor>().HasMany(x => x.Devices)
                .WithOne()
                .HasForeignKey(x => x.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            // VendorApiKey
            builder.Entity<VendorApiKey>().ToTable("VendorApiKeys");
            builder.Entity<VendorApiKey>().HasKey(x => x.Id);

            // Device
            builder.Entity<Device>().ToTable("Devices");
            builder.Entity<Device>().HasKey(x => x.Id);
            builder.Entity<Device>().HasOne(x => x.Address)
                .WithOne()
                .HasForeignKey<PostalAddress>(x => x.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Device>().HasIndex(x => new { x.Lon, x.Lat }).HasName("IX_Location");

            // PostalAddress
            builder.Entity<PostalAddress>().ToTable("PostalAddresses");
            builder.Entity<PostalAddress>().HasKey(x => x.Id);
        }
    }
}