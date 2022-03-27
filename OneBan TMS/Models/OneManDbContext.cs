using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OneBan_TMS.Models
{
    public partial class OneManDbContext : DbContext
    {
        public OneManDbContext(DbContextOptions<OneManDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyNote> CompanyNotes { get; set; }
        public virtual DbSet<CompanyNoteAttachment> CompanyNoteAttachments { get; set; }
        public virtual DbSet<Correspondence> Correspondences { get; set; }
        public virtual DbSet<CorrespondenceAttachment> CorrespondenceAttachments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePrivilege> EmployeePrivileges { get; set; }
        public virtual DbSet<EmployeePrivilegeEmployee> EmployeePrivilegeEmployees { get; set; }
        public virtual DbSet<EmployeeTeam> EmployeeTeams { get; set; }
        public virtual DbSet<EmployeeTeamRole> EmployeeTeamRoles { get; set; }
        public virtual DbSet<EmployeeTicket> EmployeeTickets { get; set; }
        public virtual DbSet<OrganizationalTask> OrganizationalTasks { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketNote> TicketNotes { get; set; }
        public virtual DbSet<TicketPriority> TicketPriorities { get; set; }
        public virtual DbSet<TicketStatus> TicketStatuses { get; set; }
        public virtual DbSet<TicketType> TicketTypes { get; set; }
        public virtual DbSet<TimeEntry> TimeEntries { get; set; }

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
                    .HasColumnName("adr_id");

                entity.Property(e => e.AdrCountry)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("adr_country");

                entity.Property(e => e.AdrIdCompany).HasColumnName("adr_idCompany");

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

                entity.HasOne(d => d.AdrIdCompanyNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.AdrIdCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Address_Company");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CmpId)
                    .HasName("Company_pk");

                entity.ToTable("Company");

                entity.Property(e => e.CmpId)
                    .ValueGeneratedNever()
                    .HasColumnName("cmp_id");

<<<<<<< HEAD
=======
                entity.Property(e => e.CmpIdAddress).HasColumnName("cmp_idAdress");

>>>>>>> main
                entity.Property(e => e.CmpKrsNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cmp_krsNumber");

                entity.Property(e => e.CmpLandline)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cmp_landline");

                entity.Property(e => e.CmpName)
                    .IsRequired()
                    .HasMaxLength(50)
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
<<<<<<< HEAD
=======

                entity.HasOne(d => d.CmpIdAddressNavigation)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.CmpIdAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("company_Address");
>>>>>>> main
            });

            modelBuilder.Entity<CompanyNote>(entity =>
            {
                entity.HasKey(e => e.CntId)
                    .HasName("CompanyNote_pk");

                entity.ToTable("CompanyNote");

                entity.Property(e => e.CntId)
                    .ValueGeneratedNever()
                    .HasColumnName("cnt_id");

                entity.Property(e => e.CntContent)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("cnt_content");

                entity.Property(e => e.CntIdCompany).HasColumnName("cnt_idCompany");

                entity.HasOne(d => d.CntIdCompanyNavigation)
                    .WithMany(p => p.CompanyNotes)
                    .HasForeignKey(d => d.CntIdCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NotatkaFirma_Firma");
            });

            modelBuilder.Entity<CompanyNoteAttachment>(entity =>
            {
                entity.HasKey(e => e.CnaId)
                    .HasName("CompanyNoteAttachment_pk");

                entity.ToTable("CompanyNoteAttachment");

                entity.Property(e => e.CnaId)
                    .ValueGeneratedNever()
                    .HasColumnName("cna_id");

                entity.Property(e => e.CnaBinaryData)
                    .IsRequired()
                    .HasColumnName("cna_binaryData");

                entity.Property(e => e.CnaIdCompanyNote).HasColumnName("cna_idCompanyNote");

                entity.Property(e => e.CnaName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cna_name");

                entity.HasOne(d => d.CnaIdCompanyNoteNavigation)
                    .WithMany(p => p.CompanyNoteAttachments)
                    .HasForeignKey(d => d.CnaIdCompanyNote)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CompanyNoteAttachment_CompanyNote");
            });

            modelBuilder.Entity<Correspondence>(entity =>
            {
                entity.HasKey(e => e.CorId)
                    .HasName("Correspondence_pk");

                entity.ToTable("Correspondence");

                entity.Property(e => e.CorId)
                    .ValueGeneratedNever()
                    .HasColumnName("cor_id");

                entity.Property(e => e.CorBody)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("cor_body");

                entity.Property(e => e.CorIdTicket).HasColumnName("cor_idTicket");

                entity.Property(e => e.CorReceivedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("cor_receivedAt");

                entity.Property(e => e.CorReceiver)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cor_receiver");

                entity.Property(e => e.CorSender)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cor_sender");

                entity.Property(e => e.CorSentAt)
                    .HasColumnType("datetime")
                    .HasColumnName("cor_sentAt");

                entity.Property(e => e.CorSubject)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cor_subject");

                entity.HasOne(d => d.CorIdTicketNavigation)
                    .WithMany(p => p.Correspondences)
                    .HasForeignKey(d => d.CorIdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Correspondence_Ticket");
            });

            modelBuilder.Entity<CorrespondenceAttachment>(entity =>
            {
                entity.HasKey(e => e.CatId)
                    .HasName("CorrespondenceAttachment_pk");

                entity.ToTable("CorrespondenceAttachment");

                entity.Property(e => e.CatId)
                    .ValueGeneratedNever()
                    .HasColumnName("cat_id");

                entity.Property(e => e.CatBinaryData)
                    .IsRequired()
                    .HasColumnName("cat_binaryData");

                entity.Property(e => e.CatIdCorrespondence).HasColumnName("cat_idCorrespondence");

                entity.Property(e => e.CatName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cat_name");

                entity.HasOne(d => d.CatIdCorrespondenceNavigation)
                    .WithMany(p => p.CorrespondenceAttachments)
                    .HasForeignKey(d => d.CatIdCorrespondence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("attachment_correspondence");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CurId)
                    .HasName("Customer_pk");

                entity.ToTable("Customer");

                entity.Property(e => e.CurId)
                    .ValueGeneratedNever()
                    .HasColumnName("cur_id");

                entity.Property(e => e.CurComments)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cur_comments");

                entity.Property(e => e.CurCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("cur_createdAt");

                entity.Property(e => e.CurEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("cur_email");

                entity.Property(e => e.CurIdCompany).HasColumnName("cur_idCompany");

                entity.Property(e => e.CurName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cur_name");

                entity.Property(e => e.CurPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cur_phoneNumber");

                entity.Property(e => e.CurPosition)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cur_position");

                entity.Property(e => e.CurSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cur_surname");

                entity.HasOne(d => d.CurIdCompanyNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CurIdCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Klient_Firma");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("Employee_pk");

                entity.ToTable("Employee");

                entity.Property(e => e.EmpId)
                    .ValueGeneratedNever()
                    .HasColumnName("emp_id");

                entity.Property(e => e.EmpCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("emp_createdAt");

                entity.Property(e => e.EmpEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("emp_email");

                entity.Property(e => e.EmpLogin)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("emp_login");

                entity.Property(e => e.EmpName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("emp_name");

                entity.Property(e => e.EmpPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("emp_password");

                entity.Property(e => e.EmpPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("emp_phoneNumber");

                entity.Property(e => e.EmpSurname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("emp_surname");
            });

            modelBuilder.Entity<EmployeePrivilege>(entity =>
            {
                entity.HasKey(e => e.EpvId)
                    .HasName("EmployeePrivilege_pk");

                entity.ToTable("EmployeePrivilege");

                entity.Property(e => e.EpvId)
                    .ValueGeneratedNever()
                    .HasColumnName("epv_id");

                entity.Property(e => e.EpvDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("epv_description");

                entity.Property(e => e.EpvName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("epv_name");
            });

            modelBuilder.Entity<EmployeePrivilegeEmployee>(entity =>
            {
                entity.HasKey(e => e.EpeId)
                    .HasName("EmployeePrivilegeEmployee_pk");

                entity.ToTable("EmployeePrivilegeEmployee");

                entity.Property(e => e.EpeId)
                    .ValueGeneratedNever()
                    .HasColumnName("epe_id");

                entity.Property(e => e.EpeIdEmployee).HasColumnName("epe_idEmployee");

                entity.Property(e => e.EpeIdEmployeePrivilage).HasColumnName("epe_idEmployeePrivilage");

                entity.HasOne(d => d.EpeIdEmployeeNavigation)
                    .WithMany(p => p.EmployeePrivilegeEmployees)
                    .HasForeignKey(d => d.EpeIdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeePrivilegesEmployee_Employee");

                entity.HasOne(d => d.EpeIdEmployeePrivilageNavigation)
                    .WithMany(p => p.EmployeePrivilegeEmployees)
                    .HasForeignKey(d => d.EpeIdEmployeePrivilage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EmployeePrivilegesEmployee_EmployeePrivileges");
            });

            modelBuilder.Entity<EmployeeTeam>(entity =>
            {
                entity.HasKey(e => e.EtmId)
                    .HasName("EmployeeTeam_pk");

                entity.ToTable("EmployeeTeam");

                entity.Property(e => e.EtmId)
                    .ValueGeneratedNever()
                    .HasColumnName("etm_id");

                entity.Property(e => e.EtmIdEmployee).HasColumnName("etm_idEmployee");

                entity.Property(e => e.EtmIdRole).HasColumnName("etm_idRole");

                entity.Property(e => e.EtmIdTeam).HasColumnName("etm_idTeam");

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

            modelBuilder.Entity<EmployeeTeamRole>(entity =>
            {
                entity.HasKey(e => e.EtrId)
                    .HasName("EmployeeTeamRole_pk");

                entity.ToTable("EmployeeTeamRole");

                entity.Property(e => e.EtrId)
                    .ValueGeneratedNever()
                    .HasColumnName("etr_id");

                entity.Property(e => e.EtrDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("etr_description");

                entity.Property(e => e.EtrName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("etr_name");
            });

            modelBuilder.Entity<EmployeeTicket>(entity =>
            {
                entity.HasKey(e => e.EtsId)
                    .HasName("EmployeeTicket_pk");

                entity.ToTable("EmployeeTicket");

                entity.Property(e => e.EtsId)
                    .ValueGeneratedNever()
                    .HasColumnName("ets_id");

                entity.Property(e => e.EtsIdEmployee).HasColumnName("ets_idEmployee");

                entity.Property(e => e.EtsIdTicket).HasColumnName("ets_idTicket");

                entity.HasOne(d => d.EtsIdEmployeeNavigation)
                    .WithMany(p => p.EmployeeTickets)
                    .HasForeignKey(d => d.EtsIdEmployee)
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
                    .HasColumnName("otk_id");

                entity.Property(e => e.OtkDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("otk_description");

                entity.Property(e => e.OtkIdEmployee).HasColumnName("otk_idEmployee");

                entity.HasOne(d => d.OtkIdEmployeeNavigation)
                    .WithMany(p => p.OrganizationalTasks)
                    .HasForeignKey(d => d.OtkIdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ZadanieOrganizacyjne_Pracownik");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProId)
                    .HasName("Project_pk");

                entity.ToTable("Project");

                entity.Property(e => e.ProId)
                    .ValueGeneratedNever()
                    .HasColumnName("pro_id");

                entity.Property(e => e.ProCompletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("pro_completedAt");

                entity.Property(e => e.ProCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("pro_createdAt");

                entity.Property(e => e.ProDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("pro_description");

                entity.Property(e => e.ProIdCompany).HasColumnName("pro_idCompany");

                entity.Property(e => e.ProIdTeam).HasColumnName("pro_idTeam");

                entity.Property(e => e.ProName)
                    .IsRequired()
                    .HasMaxLength(50)
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
                    .HasColumnName("ptk_id");

                entity.Property(e => e.PtkContent)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("ptk_content");

                entity.Property(e => e.PtkEstimatedCost)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("ptk_estimatedCost");

                entity.Property(e => e.PtkIdProject).HasColumnName("ptk_idProject");

                entity.HasOne(d => d.PtkIdProjectNavigation)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.PtkIdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ZadanieProjektu_Projekt");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TemId)
                    .HasName("Team_pk");

                entity.ToTable("Team");

                entity.Property(e => e.TemId)
                    .ValueGeneratedNever()
                    .HasColumnName("tem_id");

                entity.Property(e => e.TemName)
                    .HasMaxLength(50)
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
                    .HasColumnName("tic_id");

                entity.Property(e => e.TicCompletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("tic_completedAt");

                entity.Property(e => e.TicCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("tic_createdAt");

                entity.Property(e => e.TicDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tic_description");

                entity.Property(e => e.TicDueDate)
                    .HasColumnType("date")
                    .HasColumnName("tic_dueDate");

                entity.Property(e => e.TicEstimatedCost)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("tic_estimatedCost");

                entity.Property(e => e.TicIdCustomer).HasColumnName("tic_idCustomer");

                entity.Property(e => e.TicIdTicketPriority).HasColumnName("tic_idTicketPriority");

                entity.Property(e => e.TicIdTicketStatus).HasColumnName("tic_idTicketStatus");

                entity.Property(e => e.TicIdTicketType).HasColumnName("tic_idTicketType");

                entity.Property(e => e.TicName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tic_name");

                entity.Property(e => e.TicTopic)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tic_topic");

                entity.HasOne(d => d.TicIdCustomerNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicIdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ticket_Customer");

                entity.HasOne(d => d.TicIdTicketPriorityNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicIdTicketPriority)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ticket_TicketPriority");

                entity.HasOne(d => d.TicIdTicketStatusNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicIdTicketStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ticket_Status");

                entity.HasOne(d => d.TicIdTicketTypeNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicIdTicketType)
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
                    .HasColumnName("tnt_id");

                entity.Property(e => e.TntContent)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("tnt_content");

                entity.Property(e => e.TntIdTicket).HasColumnName("tnt_idTicket");

                entity.HasOne(d => d.TntIdTicketNavigation)
                    .WithMany(p => p.TicketNotes)
                    .HasForeignKey(d => d.TntIdTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TicketNote_Ticket");
            });

            modelBuilder.Entity<TicketPriority>(entity =>
            {
                entity.HasKey(e => e.TpiId)
                    .HasName("TicketPriority_pk");

                entity.ToTable("TicketPriority");

                entity.Property(e => e.TpiId)
                    .ValueGeneratedNever()
                    .HasColumnName("tpi_id");

                entity.Property(e => e.TpiDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tpi_description");

                entity.Property(e => e.TpiWeight).HasColumnName("tpi_weight");
            });

            modelBuilder.Entity<TicketStatus>(entity =>
            {
                entity.HasKey(e => e.TstId)
                    .HasName("TicketStatus_pk");

                entity.ToTable("TicketStatus");

                entity.Property(e => e.TstId)
                    .ValueGeneratedNever()
                    .HasColumnName("tst_id");

                entity.Property(e => e.TstDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tst_description");

                entity.Property(e => e.TstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tst_name");
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.HasKey(e => e.TtpId)
                    .HasName("TicketType_pk");

                entity.ToTable("TicketType");

                entity.Property(e => e.TtpId)
                    .ValueGeneratedNever()
                    .HasColumnName("ttp_id");

                entity.Property(e => e.TtpDescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ttp_description");

                entity.Property(e => e.TtpName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ttp_name");
            });

            modelBuilder.Entity<TimeEntry>(entity =>
            {
                entity.HasKey(e => e.TesId)
                    .HasName("TimeEntry_pk");

                entity.ToTable("TimeEntry");

                entity.Property(e => e.TesId)
                    .ValueGeneratedNever()
                    .HasColumnName("tes_id");

                entity.Property(e => e.TesCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("tes_createdAt");

                entity.Property(e => e.TesDescription)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("tes_description");

                entity.Property(e => e.TesEntryDate)
                    .HasColumnType("date")
                    .HasColumnName("tes_entryDate");

                entity.Property(e => e.TesEntryTime).HasColumnName("tes_entryTime");

                entity.Property(e => e.TesIdProjectTask).HasColumnName("tes_idProjectTask");

                entity.Property(e => e.TesIdTicket).HasColumnName("tes_idTicket");

                entity.Property(e => e.TesUpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("tes_updatedAt");

                entity.HasOne(d => d.TesIdProjectTaskNavigation)
                    .WithMany(p => p.TimeEntries)
                    .HasForeignKey(d => d.TesIdProjectTask)
                    .HasConstraintName("CzasPracy_ZadanieProjektu");

                entity.HasOne(d => d.TesIdTicketNavigation)
                    .WithMany(p => p.TimeEntries)
                    .HasForeignKey(d => d.TesIdTicket)
                    .HasConstraintName("TimeEntry_Ticket");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
