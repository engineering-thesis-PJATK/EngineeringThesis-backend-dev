using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class OneManDbContext : DbContext
    {
        public OneManDbContext()
        {
        }

        public OneManDbContext(DbContextOptions<OneManDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyNote> CompanyNotes { get; set; }
        public virtual DbSet<Correspondence> Correspondences { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeTeam> EmployeeTeams { get; set; }
        public virtual DbSet<EmployeeTicket> EmployeeTickets { get; set; }
        public virtual DbSet<OrganizationalTask> OrganizationalTasks { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketNote> TicketNotes { get; set; }
        public virtual DbSet<TimeEntry> TimeEntries { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
              //  optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AdrId)
                    .HasName("Address_pk");

                entity.ToTable("Address");

                entity.Property(e => e.AdrId)
                    .ValueGeneratedNever()
                    .HasColumnName("adr_Id");

                entity.Property(e => e.AdrPostCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("adr_postCode");

                entity.Property(e => e.AdrStreet)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("adr_street");

                entity.Property(e => e.AdrStreetNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("adr_streetNumber");

                entity.Property(e => e.AdrTown)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("adr_town");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasKey(e => e.AttId)
                    .HasName("Attachment_pk");

                entity.ToTable("Attachment");

                entity.Property(e => e.AttId)
                    .ValueGeneratedNever()
                    .HasColumnName("att_Id");

                entity.Property(e => e.AttBinarydata)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("att_binarydata");

                entity.Property(e => e.AttIdCorrespondence).HasColumnName("att_IdCorrespondence");

                entity.Property(e => e.AttName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("att_name");

                entity.HasOne(d => d.AttIdCorrespondenceNavigation)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.AttIdCorrespondence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attachment_correspondence");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CmpId)
                    .HasName("Company_pk");

                entity.ToTable("Company");

                entity.Property(e => e.CmpId)
                    .ValueGeneratedNever()
                    .HasColumnName("cmp_Id");

                entity.Property(e => e.CmpIdAdress).HasColumnName("cmp_IdAdress");

                entity.Property(e => e.CmpKrsNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cmp_krsNumber");

                entity.Property(e => e.CmpLandline)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cmp_landline");

                entity.Property(e => e.CmpName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("cmp_name");

                entity.Property(e => e.CmpNip)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cmp_nip");

                entity.Property(e => e.CmpNipPrefix)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("cmp_nipPrefix");

                entity.Property(e => e.CmpRegon)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cmp_regon");

                entity.HasOne(d => d.CmpIdAdressNavigation)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CmpIdAdress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("company_Address");
            });

            modelBuilder.Entity<CompanyNote>(entity =>
            {
                entity.HasKey(e => e.CntId)
                    .HasName("CompanyNote_pk");

                entity.ToTable("CompanyNote");

                entity.Property(e => e.CntId)
                    .ValueGeneratedNever()
                    .HasColumnName("cnt_id");

                entity.Property(e => e.CndContent)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("cnd_content");

                entity.Property(e => e.CntIdCompany).HasColumnName("cnt_IdCompany");

                entity.HasOne(d => d.CntIdCompanyNavigation)
                    .WithMany(p => p.CompanyNotes)
                    .HasForeignKey(d => d.CntIdCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NotatkaFirma_Firma");
            });

            modelBuilder.Entity<Correspondence>(entity =>
            {
                entity.HasKey(e => e.CorId)
                    .HasName("Correspondence_pk");

                entity.ToTable("Correspondence");

                entity.Property(e => e.CorId)
                    .ValueGeneratedNever()
                    .HasColumnName("cor_Id");

                entity.Property(e => e.CorBody).HasColumnName("cor_body");

                entity.Property(e => e.CorIdTicket).HasColumnName("cor_IdTicket");

                entity.Property(e => e.CorReceived)
                    .HasColumnType("datetime")
                    .HasColumnName("cor_received");

                entity.Property(e => e.CorReceiver)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("cor_receiver");

                entity.Property(e => e.CorSender)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cor_sender");

                entity.Property(e => e.CorSent)
                    .HasColumnType("datetime")
                    .HasColumnName("cor_sent");

                entity.Property(e => e.CorSubject).HasColumnName("cor_subject");

                entity.HasOne(d => d.CorIdTicketNavigation)
                    .WithMany(p => p.Correspondences)
                    .HasForeignKey(d => d.CorIdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Correspondence_Ticket");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CurId)
                    .HasName("Customer_pk");

                entity.ToTable("Customer");

                entity.Property(e => e.CurId)
                    .ValueGeneratedNever()
                    .HasColumnName("cur_Id");

                entity.Property(e => e.CurComments)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("cur_comments");

                entity.Property(e => e.CurIdCompany).HasColumnName("cur_IdCompany");

                entity.Property(e => e.CurIdPerson).HasColumnName("cur_IdPerson");

                entity.Property(e => e.CurPosition)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cur_position");

                entity.HasOne(d => d.CurIdCompanyNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CurIdCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Klient_Firma");

                entity.HasOne(d => d.CurIdPersonNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CurIdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Klient_Osoba");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("Employee_pk");

                entity.ToTable("Employee");

                entity.Property(e => e.EmpId)
                    .ValueGeneratedNever()
                    .HasColumnName("emp_Id");

                entity.Property(e => e.EmpIdPerson).HasColumnName("emp_IdPerson");

                entity.Property(e => e.EmpIsAdmin).HasColumnName("emp_isAdmin");

                entity.Property(e => e.EmpLogin)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_login");

                entity.Property(e => e.EmpPassword)
                    .IsRequired()
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("emp_password");

                entity.Property(e => e.EmpPrivelages).HasColumnName("emp_privelages");

                entity.HasOne(d => d.EmpIdPersonNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmpIdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pracownik_Osoba");
            });

            modelBuilder.Entity<EmployeeTeam>(entity =>
            {
                entity.HasKey(e => e.EtmId)
                    .HasName("EmployeeTeam_pk");

                entity.ToTable("EmployeeTeam");

                entity.Property(e => e.EtmId)
                    .ValueGeneratedNever()
                    .HasColumnName("etm_Id");

                entity.Property(e => e.EtmIdEmployee).HasColumnName("etm_IdEmployee");

                entity.Property(e => e.EtmIdRole).HasColumnName("etm_IdRole");

                entity.Property(e => e.EtmIdTeam).HasColumnName("etm_IdTeam");

                entity.HasOne(d => d.EtmIdEmployeeNavigation)
                    .WithMany(p => p.EmployeeTeams)
                    .HasForeignKey(d => d.EtmIdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PrcZspElem_Pracownik");

                entity.HasOne(d => d.EtmIdRoleNavigation)
                    .WithMany(p => p.EmployeeTeams)
                    .HasForeignKey(d => d.EtmIdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PrcZspElem_Role");

                entity.HasOne(d => d.EtmIdTeamNavigation)
                    .WithMany(p => p.EmployeeTeams)
                    .HasForeignKey(d => d.EtmIdTeam)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PrcZspElem_Zespol");
            });

            modelBuilder.Entity<EmployeeTicket>(entity =>
            {
                entity.HasKey(e => e.EtsId)
                    .HasName("EmployeeTicket_pk");

                entity.ToTable("EmployeeTicket");

                entity.Property(e => e.EtsId)
                    .ValueGeneratedNever()
                    .HasColumnName("ets_Id");

                entity.Property(e => e.EtsEmployeeId).HasColumnName("ets_employeeId");

                entity.Property(e => e.EtsIdTicket).HasColumnName("ets_IdTicket");

                entity.HasOne(d => d.EtsEmployee)
                    .WithMany(p => p.EmployeeTickets)
                    .HasForeignKey(d => d.EtsEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PracownicyZgloszenia_Pracownik");

                entity.HasOne(d => d.EtsIdTicketNavigation)
                    .WithMany(p => p.EmployeeTickets)
                    .HasForeignKey(d => d.EtsIdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeeTicket_Ticket");
            });

            modelBuilder.Entity<OrganizationalTask>(entity =>
            {
                entity.HasKey(e => e.OtkId)
                    .HasName("OrganizationalTask_pk");

                entity.ToTable("OrganizationalTask");

                entity.Property(e => e.OtkId)
                    .ValueGeneratedNever()
                    .HasColumnName("otk_Id");

                entity.Property(e => e.OtkDescription)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("otk_description");

                entity.Property(e => e.OtkIdEmployee).HasColumnName("otk_IdEmployee");

                entity.HasOne(d => d.OtkIdEmployeeNavigation)
                    .WithMany(p => p.OrganizationalTasks)
                    .HasForeignKey(d => d.OtkIdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ZadanieOrganizacyjne_Pracownik");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PerId)
                    .HasName("Person_pk");

                entity.ToTable("Person");

                entity.Property(e => e.PerId)
                    .ValueGeneratedNever()
                    .HasColumnName("per_id");

                entity.Property(e => e.PerAccountCreationTimestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("per_AccountCreationTimestamp");

                entity.Property(e => e.PerEmail)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("per_email");

                entity.Property(e => e.PerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("per_name");

                entity.Property(e => e.PerPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("per_phoneNumber");

                entity.Property(e => e.PerSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("per_surname");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProId)
                    .HasName("Project_pk");

                entity.ToTable("Project");

                entity.Property(e => e.ProId)
                    .ValueGeneratedNever()
                    .HasColumnName("pro_Id");

                entity.Property(e => e.ProCompletionDate)
                    .HasColumnType("date")
                    .HasColumnName("pro_completionDate");

                entity.Property(e => e.ProCreationDate)
                    .HasColumnType("date")
                    .HasColumnName("pro_creationDate");

                entity.Property(e => e.ProDescription)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("pro_description");

                entity.Property(e => e.ProIdCompany).HasColumnName("pro_IdCompany");

                entity.Property(e => e.ProIdTeam).HasColumnName("pro_IdTeam");

                entity.Property(e => e.ProName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pro_name");

                entity.HasOne(d => d.ProIdCompanyNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProIdCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Projekt_Firma");

                entity.HasOne(d => d.ProIdTeamNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProIdTeam)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Projekt_Zespol");
            });

            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.HasKey(e => e.PtkId)
                    .HasName("ProjectTask_pk");

                entity.ToTable("ProjectTask");

                entity.Property(e => e.PtkId)
                    .ValueGeneratedNever()
                    .HasColumnName("ptk_Id");

                entity.Property(e => e.PtkContent)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("ptk_content");

                entity.Property(e => e.PtkEstCost)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("ptk_estCost");

                entity.Property(e => e.PtkProjectId).HasColumnName("ptk_projectId");

                entity.HasOne(d => d.PtkProject)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.PtkProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ZadanieProjektu_Projekt");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RolId)
                    .HasName("Role_pk");

                entity.ToTable("Role");

                entity.Property(e => e.RolId)
                    .ValueGeneratedNever()
                    .HasColumnName("rol_Id");

                entity.Property(e => e.RolDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rol_description");

                entity.Property(e => e.RolName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("rol_name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.StsId)
                    .HasName("Status_pk");

                entity.ToTable("Status");

                entity.Property(e => e.StsId)
                    .ValueGeneratedNever()
                    .HasColumnName("Sts_Id");

                entity.Property(e => e.StsDescriotion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Sts_Descriotion");

                entity.Property(e => e.StsName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Sts_Name");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TemId)
                    .HasName("Team_pk");

                entity.ToTable("Team");

                entity.Property(e => e.TemId)
                    .ValueGeneratedNever()
                    .HasColumnName("tem_Id");

                entity.Property(e => e.TemName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("tem_name");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicId)
                    .HasName("Ticket_pk");

                entity.ToTable("Ticket");

                entity.Property(e => e.TicId)
                    .ValueGeneratedNever()
                    .HasColumnName("tic_Id");

                entity.Property(e => e.TicCreationTime)
                    .HasColumnType("date")
                    .HasColumnName("tic_creationTime");

                entity.Property(e => e.TicDescription)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("tic_description");

                entity.Property(e => e.TicDueTime)
                    .HasColumnType("date")
                    .HasColumnName("tic_dueTime");

                entity.Property(e => e.TicEstCost)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("tic_estCost");

                entity.Property(e => e.TicIdClient).HasColumnName("tic_IdClient");

                entity.Property(e => e.TicIdStatus).HasColumnName("tic_idStatus");

                entity.Property(e => e.TicIdType).HasColumnName("tic_IdType");

                entity.Property(e => e.TicName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tic_name");

                entity.Property(e => e.TicPriority).HasColumnName("tic_priority");

                entity.Property(e => e.TicTittle)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tic_tittle");

                entity.HasOne(d => d.TicIdClientNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicIdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ticket_Customer");

                entity.HasOne(d => d.TicIdStatusNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicIdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ticket_Status");

                entity.HasOne(d => d.TicIdTypeNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicIdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ticket_Type");
            });

            modelBuilder.Entity<TicketNote>(entity =>
            {
                entity.HasKey(e => e.TntId)
                    .HasName("TicketNote_pk");

                entity.ToTable("TicketNote");

                entity.Property(e => e.TntId)
                    .ValueGeneratedNever()
                    .HasColumnName("tnt_Id");

                entity.Property(e => e.TntContent)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("tnt_content");

                entity.Property(e => e.TntIdTicket).HasColumnName("tnt_IdTicket");

                entity.HasOne(d => d.TntIdTicketNavigation)
                    .WithMany(p => p.TicketNotes)
                    .HasForeignKey(d => d.TntIdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TicketNote_Ticket");
            });

            modelBuilder.Entity<TimeEntry>(entity =>
            {
                entity.HasKey(e => e.TesId)
                    .HasName("TimeEntry_pk");

                entity.ToTable("TimeEntry");

                entity.Property(e => e.TesId)
                    .ValueGeneratedNever()
                    .HasColumnName("tes_Id");

                entity.Property(e => e.TesCompletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("tes_completionDate");

                entity.Property(e => e.TesEntryDate)
                    .HasColumnType("date")
                    .HasColumnName("tes_entryDate");

                entity.Property(e => e.TesEntryTime).HasColumnName("tes_entryTime");

                entity.Property(e => e.TesIdTicket).HasColumnName("tes_IdTicket");

                entity.Property(e => e.TesProjectTask).HasColumnName("tes_projectTask");

                entity.Property(e => e.TesStartingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("tes_startingDate");

                entity.HasOne(d => d.TesIdTicketNavigation)
                    .WithMany(p => p.TimeEntries)
                    .HasForeignKey(d => d.TesIdTicket)
                    .HasConstraintName("TimeEntry_Ticket");

                entity.HasOne(d => d.TesProjectTaskNavigation)
                    .WithMany(p => p.TimeEntries)
                    .HasForeignKey(d => d.TesProjectTask)
                    .HasConstraintName("CzasPracy_ZadanieProjektu");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.TypId)
                    .HasName("Type_pk");

                entity.ToTable("Type");

                entity.Property(e => e.TypId)
                    .ValueGeneratedNever()
                    .HasColumnName("Typ_Id");

                entity.Property(e => e.TypDescription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Typ_description");

                entity.Property(e => e.TypName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Typ_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
