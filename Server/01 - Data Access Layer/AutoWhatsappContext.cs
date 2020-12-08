using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tomedia
{
    public partial class AutoWhatsappContext : DbContext
    {
        public AutoWhatsappContext()
        {
        }

        public AutoWhatsappContext(DbContextOptions<AutoWhatsappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AutoMessage> AutoMessages { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<ChatBot> ChatBots { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<MailingList> MailingLists { get; set; }
        public virtual DbSet<MailingListsContact> MailingListsContacts { get; set; }
        public virtual DbSet<MailingListsMessage> MailingListsMessages { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SqlExpress;Database=AutoWhatsapp;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.AnswerContent)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<AutoMessage>(entity =>
            {
                entity.Property(e => e.AutoMessageId).HasColumnName("AutoMessageID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.AutoMessages)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AutoMessages_Messages");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.AutoMessages)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AutoMessages_Schedules");
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.BusinessEmail).HasMaxLength(50);

                entity.Property(e => e.BusinessName).HasMaxLength(50);

                entity.Property(e => e.BusinessPhone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.BusinessType).HasMaxLength(50);

                entity.Property(e => e.ContactsBookFileName).HasMaxLength(50);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ChatBot>(entity =>
            {
                entity.Property(e => e.ChatBotId).HasColumnName("ChatBotID");

                entity.Property(e => e.MessageAnswerId).HasColumnName("MessageAnswerID");

                entity.Property(e => e.MessageQuestionId).HasColumnName("MessageQuestionID");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.ContactEmail).HasMaxLength(50);

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.BusinessId)
                    .HasConstraintName("FK_Contacts_Businesses");
            });

            modelBuilder.Entity<MailingList>(entity =>
            {
                entity.Property(e => e.MailingListId).HasColumnName("MailingListID");

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.MailingListName).HasMaxLength(20);
            });

            modelBuilder.Entity<MailingListsContact>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.MailingListId).HasColumnName("MailingListID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.MailingListsContacts)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MailingListsContacts_Contacts");

                entity.HasOne(d => d.MailingList)
                    .WithMany(p => p.MailingListsContacts)
                    .HasForeignKey(d => d.MailingListId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MailingListsContacts_MailingLists");
            });

            modelBuilder.Entity<MailingListsMessage>(entity =>
            {
                entity.HasKey(e => e.MailingListMessageId);

                entity.Property(e => e.MailingListMessageId).HasColumnName("MailingListMessageID");

                entity.Property(e => e.MailingListId).HasColumnName("MailingListID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.HasOne(d => d.MailingList)
                    .WithMany(p => p.MailingListsMessages)
                    .HasForeignKey(d => d.MailingListId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MailingListsMessages_MailingLists");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MailingListsMessages)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MailingListsMessages_Messages");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.FilePath).HasMaxLength(300);

                entity.Property(e => e.MessageContent)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.BusinessId)
                    .HasConstraintName("FK_Messages_Businesses");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.BusinessId).HasColumnName("BusinessID");

                entity.Property(e => e.QuestionContent)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Answers");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Businesses");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");

                entity.Property(e => e.FromTime).HasColumnType("date");

                entity.Property(e => e.ToTime).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
