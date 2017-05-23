using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ASPNETProject2.Data;

namespace ASPNETProject2.Migrations
{
    [DbContext(typeof(RatingContext))]
    [Migration("20170519143529_RatingContext_6.0")]
    partial class RatingContext_60
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASPNETProject2.Models.Contractor", b =>
                {
                    b.Property<int>("ContractorID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AverageRating");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<int>("ReviewCount");

                    b.Property<int>("TradeID");

                    b.HasKey("ContractorID");

                    b.HasIndex("TradeID");

                    b.ToTable("Contractor");
                });

            modelBuilder.Entity("ASPNETProject2.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ASPNETProject2.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContractorID");

                    b.Property<int>("CustomerID");

                    b.Property<int>("Rating");

                    b.Property<string>("message")
                        .IsRequired();

                    b.HasKey("ReviewID");

                    b.HasIndex("ContractorID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("ASPNETProject2.Models.Trade", b =>
                {
                    b.Property<int>("TradeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TradeType");

                    b.HasKey("TradeID");

                    b.ToTable("Trade");
                });

            modelBuilder.Entity("ASPNETProject2.Models.Contractor", b =>
                {
                    b.HasOne("ASPNETProject2.Models.Trade", "Trade")
                        .WithMany("Contractors")
                        .HasForeignKey("TradeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ASPNETProject2.Models.Review", b =>
                {
                    b.HasOne("ASPNETProject2.Models.Contractor", "Contractor")
                        .WithMany("Reviews")
                        .HasForeignKey("ContractorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ASPNETProject2.Models.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
