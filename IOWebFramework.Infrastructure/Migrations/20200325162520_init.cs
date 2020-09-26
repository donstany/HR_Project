using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cdn_files",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    source_type = table.Column<int>(nullable: false),
                    source_id = table.Column<string>(nullable: false),
                    content_id = table.Column<string>(maxLength: 50, nullable: true),
                    file_name = table.Column<string>(maxLength: 500, nullable: false),
                    file_title = table.Column<string>(nullable: true),
                    file_description = table.Column<string>(nullable: true),
                    file_size = table.Column<int>(nullable: false),
                    date_uploaded = table.Column<DateTime>(nullable: false),
                    user_uploaded = table.Column<string>(maxLength: 500, nullable: true),
                    tenant_id = table.Column<string>(maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cdn_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "d_certificate_name_issuer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'8', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_certificate_name_issuer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "d_classifiers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'1819', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_classifiers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "d_persons",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'7', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(nullable: true),
                    pid = table.Column<string>(nullable: false),
                    photo = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "identity_roles",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "identity_users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_name = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(maxLength: 256, nullable: true),
                    email = table.Column<string>(maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false),
                    must_change_password = table.Column<bool>(nullable: false, defaultValue: false),
                    user_name_from_active_directory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "log_operations",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    controller = table.Column<string>(maxLength: 500, nullable: false),
                    action = table.Column<string>(maxLength: 500, nullable: false),
                    object_key = table.Column<string>(maxLength: 50, nullable: false),
                    master_key = table.Column<string>(maxLength: 50, nullable: true),
                    operation_type_id = table.Column<int>(nullable: false),
                    user_data = table.Column<string>(nullable: true),
                    operation_date = table.Column<DateTime>(nullable: false),
                    operation_user = table.Column<string>(maxLength: 500, nullable: true),
                    operation_user_id = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log_operations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_branches",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'28', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_branches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_certificate_types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'5', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_certificate_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_degrees",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'4', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_degrees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_education_institutions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'6', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_education_institutions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_positions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'6', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_project_role",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_project_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_projects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_school_profiles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_school_profiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_technologies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_technologies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_training_centers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'4', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_training_centers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nom_training_names",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'5', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_training_names", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cdn_file_contents",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cdn_file_id = table.Column<long>(nullable: false),
                    content = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cdn_file_contents", x => x.id);
                    table.ForeignKey(
                        name: "fk_cdn_file_contents_cdn_files_cdn_file_id",
                        column: x => x.cdn_file_id,
                        principalTable: "cdn_files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_role_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_application_role_claim_application_role_role_id",
                        column: x => x.role_id,
                        principalTable: "identity_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(nullable: false),
                    claim_type = table.Column<string>(nullable: true),
                    claim_value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_user_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_application_user_claim_application_user_user_id",
                        column: x => x.user_id,
                        principalTable: "identity_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(maxLength: 128, nullable: false),
                    provider_key = table.Column<string>(maxLength: 128, nullable: false),
                    provider_display_name = table.Column<string>(nullable: true),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_user_logins", x => new { x.provider_key, x.login_provider });
                    table.ForeignKey(
                        name: "fk_application_user_login_application_user_user_id",
                        column: x => x.user_id,
                        principalTable: "identity_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    role_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_application_user_role_application_role_role_id",
                        column: x => x.role_id,
                        principalTable: "identity_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_application_user_role_application_user_user_id",
                        column: x => x.user_id,
                        principalTable: "identity_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    login_provider = table.Column<string>(maxLength: 128, nullable: false),
                    name = table.Column<string>(maxLength: 128, nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_application_user_token_application_user_user_id",
                        column: x => x.user_id,
                        principalTable: "identity_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nom_departments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'52', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    branch_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_departments", x => x.id);
                    table.ForeignKey(
                        name: "fk_nom_departments_nom_branches_branch_id",
                        column: x => x.branch_id,
                        principalTable: "nom_branches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "d_certificates",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_active = table.Column<bool>(nullable: false),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    reg_number = table.Column<string>(nullable: true),
                    grade = table.Column<string>(maxLength: 50, nullable: true),
                    level = table.Column<string>(nullable: true),
                    file_content_id = table.Column<string>(nullable: true),
                    certificate_name_issuer_id = table.Column<int>(nullable: false),
                    certificate_type_id = table.Column<int>(nullable: false),
                    person_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_certificates", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_certificates_d_certificate_name_issuer_certificate_name_i",
                        column: x => x.certificate_name_issuer_id,
                        principalTable: "d_certificate_name_issuer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_certificates_nom_certificate_types_certificate_type_id",
                        column: x => x.certificate_type_id,
                        principalTable: "nom_certificate_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_certificates_d_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "d_persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "d_diplomas",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    issue_date = table.Column<DateTime>(nullable: false),
                    successful_exam = table.Column<DateTime>(nullable: false),
                    grade = table.Column<string>(maxLength: 4, nullable: true),
                    register_number = table.Column<string>(nullable: true),
                    file_content_id = table.Column<string>(nullable: true),
                    education_institution_id = table.Column<int>(nullable: false),
                    degree_id = table.Column<int>(nullable: false),
                    school_profile_id = table.Column<int>(nullable: true),
                    specialty_id = table.Column<int>(nullable: true),
                    person_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_diplomas", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_diplomas_nom_degrees_degree_id",
                        column: x => x.degree_id,
                        principalTable: "nom_degrees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_diplomas_nom_education_institutions_education_institution",
                        column: x => x.education_institution_id,
                        principalTable: "nom_education_institutions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_diplomas_d_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "d_persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_diplomas_nom_school_profiles_school_profile_id",
                        column: x => x.school_profile_id,
                        principalTable: "nom_school_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_d_diplomas_d_classifiers_specialty_id",
                        column: x => x.specialty_id,
                        principalTable: "d_classifiers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "d_trainings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_active = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    file_content_id = table.Column<string>(nullable: true),
                    person_id = table.Column<int>(nullable: false),
                    training_center_id = table.Column<int>(nullable: false),
                    training_name_id = table.Column<int>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_trainings", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_trainings_d_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "d_persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_trainings_nom_training_centers_training_center_id",
                        column: x => x.training_center_id,
                        principalTable: "nom_training_centers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_trainings_nom_training_names_training_name_id",
                        column: x => x.training_name_id,
                        principalTable: "nom_training_names",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "d_employees",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'7', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    td = table.Column<string>(nullable: false),
                    file_content_id = table.Column<string>(nullable: true),
                    hire_date = table.Column<DateTime>(nullable: false),
                    fire_date = table.Column<DateTime>(nullable: true),
                    previuos_experience_summed = table.Column<int>(nullable: true),
                    previuos_experience_in_is = table.Column<int>(nullable: true),
                    previuos_experience = table.Column<int>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    position = table.Column<string>(nullable: true),
                    branch = table.Column<string>(nullable: true),
                    is_leaved = table.Column<bool>(nullable: false),
                    departament = table.Column<string>(nullable: true),
                    person_id = table.Column<int>(nullable: false),
                    department_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_employees_nom_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "nom_departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_d_employees_d_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "d_persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "d_project_details",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_id = table.Column<int>(nullable: false),
                    project_id = table.Column<int>(nullable: false),
                    project_role_id = table.Column<int>(nullable: false),
                    technology_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_project_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_project_details_d_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "d_employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_project_details_nom_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "nom_projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_project_details_nom_project_role_project_role_id",
                        column: x => x.project_role_id,
                        principalTable: "nom_project_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_project_details_nom_technologies_technology_id",
                        column: x => x.technology_id,
                        principalTable: "nom_technologies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cdn_file_contents_cdn_file_id",
                table: "cdn_file_contents",
                column: "cdn_file_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_certificates_certificate_name_issuer_id",
                table: "d_certificates",
                column: "certificate_name_issuer_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_certificates_certificate_type_id",
                table: "d_certificates",
                column: "certificate_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_certificates_person_id",
                table: "d_certificates",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_diplomas_degree_id",
                table: "d_diplomas",
                column: "degree_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_diplomas_education_institution_id",
                table: "d_diplomas",
                column: "education_institution_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_diplomas_person_id",
                table: "d_diplomas",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_diplomas_school_profile_id",
                table: "d_diplomas",
                column: "school_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_diplomas_specialty_id",
                table: "d_diplomas",
                column: "specialty_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_employees_department_id",
                table: "d_employees",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_employees_person_id",
                table: "d_employees",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_employees_td",
                table: "d_employees",
                column: "td",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_d_project_details_employee_id",
                table: "d_project_details",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_project_details_project_id",
                table: "d_project_details",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_project_details_project_role_id",
                table: "d_project_details",
                column: "project_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_project_details_technology_id",
                table: "d_project_details",
                column: "technology_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_trainings_person_id",
                table: "d_trainings",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_trainings_training_center_id",
                table: "d_trainings",
                column: "training_center_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_trainings_training_name_id",
                table: "d_trainings",
                column: "training_name_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_role_claim_role_id",
                table: "identity_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_name_index",
                table: "identity_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_application_user_claim_user_id",
                table: "identity_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_user_login_user_id",
                table: "identity_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_application_user_role_role_id",
                table: "identity_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "email_index",
                table: "identity_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "user_name_index",
                table: "identity_users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_nom_departments_branch_id",
                table: "nom_departments",
                column: "branch_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cdn_file_contents");

            migrationBuilder.DropTable(
                name: "d_certificates");

            migrationBuilder.DropTable(
                name: "d_diplomas");

            migrationBuilder.DropTable(
                name: "d_project_details");

            migrationBuilder.DropTable(
                name: "d_trainings");

            migrationBuilder.DropTable(
                name: "identity_role_claims");

            migrationBuilder.DropTable(
                name: "identity_user_claims");

            migrationBuilder.DropTable(
                name: "identity_user_logins");

            migrationBuilder.DropTable(
                name: "identity_user_roles");

            migrationBuilder.DropTable(
                name: "identity_user_tokens");

            migrationBuilder.DropTable(
                name: "log_operations");

            migrationBuilder.DropTable(
                name: "nom_positions");

            migrationBuilder.DropTable(
                name: "cdn_files");

            migrationBuilder.DropTable(
                name: "d_certificate_name_issuer");

            migrationBuilder.DropTable(
                name: "nom_certificate_types");

            migrationBuilder.DropTable(
                name: "nom_degrees");

            migrationBuilder.DropTable(
                name: "nom_education_institutions");

            migrationBuilder.DropTable(
                name: "nom_school_profiles");

            migrationBuilder.DropTable(
                name: "d_classifiers");

            migrationBuilder.DropTable(
                name: "d_employees");

            migrationBuilder.DropTable(
                name: "nom_projects");

            migrationBuilder.DropTable(
                name: "nom_project_role");

            migrationBuilder.DropTable(
                name: "nom_technologies");

            migrationBuilder.DropTable(
                name: "nom_training_centers");

            migrationBuilder.DropTable(
                name: "nom_training_names");

            migrationBuilder.DropTable(
                name: "identity_roles");

            migrationBuilder.DropTable(
                name: "identity_users");

            migrationBuilder.DropTable(
                name: "nom_departments");

            migrationBuilder.DropTable(
                name: "d_persons");

            migrationBuilder.DropTable(
                name: "nom_branches");
        }
    }
}
