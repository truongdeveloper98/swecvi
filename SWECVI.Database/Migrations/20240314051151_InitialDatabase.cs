using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWECVI.Database.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentReportReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentReportReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentTextReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionReportText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallFunction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportTextSE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DCode = table.Column<int>(type: "int", nullable: false),
                    ACode = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentTextReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComonDicomTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Element = table.Column<int>(type: "int", nullable: false),
                    Tag = table.Column<double>(type: "float", nullable: false),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComonDicomTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Findings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndexExam = table.Column<int>(type: "int", nullable: false),
                    TricuspidValveFinding = table.Column<int>(type: "int", nullable: false),
                    MitralValveFinding = table.Column<int>(type: "int", nullable: false),
                    AorticValveFinding = table.Column<int>(type: "int", nullable: false),
                    PulmonicValveFinding = table.Column<int>(type: "int", nullable: false),
                    ECGFinding = table.Column<int>(type: "int", nullable: false),
                    IndexDiagnose = table.Column<int>(type: "int", nullable: false),
                    PatientStatus = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Findings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FindingStructure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowOrder = table.Column<int>(type: "int", nullable: false),
                    TabName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoxHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputType = table.Column<int>(type: "int", nullable: false),
                    InputOptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderInReport = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindingStructure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementContexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementContexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParameterSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowInChart = table.Column<bool>(type: "bit", nullable: false),
                    ShowInParameterTable = table.Column<bool>(type: "bit", nullable: true),
                    ShowInAssessmentText = table.Column<bool>(type: "bit", nullable: true),
                    TableFriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextFriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatabaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterHeader = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterSubHeader = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayDecimal = table.Column<int>(type: "int", nullable: true),
                    ParameterOrder = table.Column<int>(type: "int", nullable: true),
                    ParameterHeaderOrder = table.Column<int>(type: "int", nullable: true),
                    POH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionSelector = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PythonDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Script = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PythonDefaults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityId = table.Column<int>(type: "int", nullable: false),
                    IndexDepartment = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_AspNetUsers_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DicomTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CSD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SNOMED = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexContextID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicomTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DicomTags_MeasurementContexts_IndexContextID",
                        column: x => x.IndexContextID,
                        principalTable: "MeasurementContexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Studies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    StudyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudyInstanceUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    SOPClassUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SOPInstanceUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstitutionalDepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModalitiesInStudy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QualityControlSubject = table.Column<bool>(type: "bit", nullable: false),
                    BodySurfaceArea = table.Column<float>(type: "real", nullable: false),
                    IndexDepartment = table.Column<int>(type: "int", nullable: false),
                    SystolicBloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiastoilccBloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufactureModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studies_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PythonCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Script = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCurrentVersion = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PythonDefaultId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PythonCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PythonCodes_PythonDefaults_PythonDefaultId",
                        column: x => x.PythonDefaultId,
                        principalTable: "PythonDefaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexRegion = table.Column<int>(type: "int", nullable: false),
                    IndexDepartment = table.Column<int>(type: "int", nullable: true),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospitals_Regions_IndexRegion",
                        column: x => x.IndexRegion,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturerDicomParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderParameterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderParameterShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterNameLogic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementCSD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementCV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementCM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FindingSite = table.Column<int>(type: "int", nullable: true),
                    ImageMode = table.Column<int>(type: "int", nullable: true),
                    ImageView = table.Column<int>(type: "int", nullable: true),
                    CardiacPhase = table.Column<int>(type: "int", nullable: true),
                    MeasurementMethod = table.Column<int>(type: "int", nullable: true),
                    FlowDirection = table.Column<int>(type: "int", nullable: true),
                    AnatomicalSite = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturerDicomParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturerDicomParameters_DicomTags_CardiacPhase",
                        column: x => x.CardiacPhase,
                        principalTable: "DicomTags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManufacturerDicomParameters_DicomTags_FindingSite",
                        column: x => x.FindingSite,
                        principalTable: "DicomTags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManufacturerDicomParameters_DicomTags_FlowDirection",
                        column: x => x.FlowDirection,
                        principalTable: "DicomTags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManufacturerDicomParameters_DicomTags_ImageMode",
                        column: x => x.ImageMode,
                        principalTable: "DicomTags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManufacturerDicomParameters_DicomTags_ImageView",
                        column: x => x.ImageView,
                        principalTable: "DicomTags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManufacturerDicomParameters_DicomTags_MeasurementMethod",
                        column: x => x.MeasurementMethod,
                        principalTable: "DicomTags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudyFinding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyId = table.Column<int>(type: "int", nullable: false),
                    FindingStructureId = table.Column<int>(type: "int", nullable: false),
                    SelectOptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyFinding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyFinding_FindingStructure_FindingStructureId",
                        column: x => x.FindingStructureId,
                        principalTable: "FindingStructure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyFinding_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultValue = table.Column<float>(type: "real", nullable: false),
                    FindingSite = table.Column<int>(type: "int", nullable: false),
                    ImageView = table.Column<int>(type: "int", nullable: false),
                    ImageMode = table.Column<int>(type: "int", nullable: false),
                    CardiacCyclePoint = table.Column<int>(type: "int", nullable: false),
                    RespiratoryCyclePoint = table.Column<int>(type: "int", nullable: false),
                    MeasurementMethod = table.Column<int>(type: "int", nullable: false),
                    IndexMeasurementUnit = table.Column<int>(type: "int", nullable: false),
                    Derivation = table.Column<int>(type: "int", nullable: false),
                    SelectionStatus = table.Column<int>(type: "int", nullable: false),
                    DirectionOfFlow = table.Column<int>(type: "int", nullable: false),
                    StudyIndex = table.Column<int>(type: "int", nullable: false),
                    ValueUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyParameters_Studies_StudyIndex",
                        column: x => x.StudyIndex,
                        principalTable: "Studies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendingUnit = table.Column<int>(type: "int", nullable: false),
                    Modality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexHospital = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Hospitals_IndexHospital",
                        column: x => x.IndexHospital,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParameterReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterNameLogic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepaermentId = table.Column<double>(type: "float", nullable: true),
                    AgeFrom = table.Column<double>(type: "float", nullable: true),
                    AgeTo = table.Column<double>(type: "float", nullable: true),
                    NormalRangeLower = table.Column<double>(type: "float", nullable: true),
                    NormalRangeUpper = table.Column<double>(type: "float", nullable: true),
                    MildlyAbnormalRangeLower = table.Column<double>(type: "float", nullable: true),
                    MildlyAbnormalRangeUpper = table.Column<double>(type: "float", nullable: true),
                    ModeratelyAbnormalRangeLower = table.Column<double>(type: "float", nullable: true),
                    ModeratelyAbnormalRangeUpper = table.Column<double>(type: "float", nullable: true),
                    SeverelyAbnormalRangeMoreThan = table.Column<double>(type: "float", nullable: true),
                    SeverelyAbnormalRangeLessThan = table.Column<double>(type: "float", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    StudyParameterId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterReferences_StudyParameters_StudyParameterId",
                        column: x => x.StudyParameterId,
                        principalTable: "StudyParameters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_IndexHospital",
                table: "Departments",
                column: "IndexHospital");

            migrationBuilder.CreateIndex(
                name: "IX_DicomTags_IndexContextID",
                table: "DicomTags",
                column: "IndexContextID");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_IndexRegion",
                table: "Hospitals",
                column: "IndexRegion");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerDicomParameters_CardiacPhase",
                table: "ManufacturerDicomParameters",
                column: "CardiacPhase");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerDicomParameters_FindingSite",
                table: "ManufacturerDicomParameters",
                column: "FindingSite");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerDicomParameters_FlowDirection",
                table: "ManufacturerDicomParameters",
                column: "FlowDirection");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerDicomParameters_ImageMode",
                table: "ManufacturerDicomParameters",
                column: "ImageMode");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerDicomParameters_ImageView",
                table: "ManufacturerDicomParameters",
                column: "ImageView");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturerDicomParameters_MeasurementMethod",
                table: "ManufacturerDicomParameters",
                column: "MeasurementMethod");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterReferences_StudyParameterId",
                table: "ParameterReferences",
                column: "StudyParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_PythonCodes_PythonDefaultId",
                table: "PythonCodes",
                column: "PythonDefaultId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_PatientId",
                table: "Studies",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyFinding_FindingStructureId",
                table: "StudyFinding",
                column: "FindingStructureId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyFinding_StudyId",
                table: "StudyFinding",
                column: "StudyId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyParameters_StudyIndex",
                table: "StudyParameters",
                column: "StudyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityId",
                table: "Users",
                column: "IdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssessmentReportReferences");

            migrationBuilder.DropTable(
                name: "AssessmentTextReferences");

            migrationBuilder.DropTable(
                name: "ComonDicomTags");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Findings");

            migrationBuilder.DropTable(
                name: "ManufacturerDicomParameters");

            migrationBuilder.DropTable(
                name: "ParameterReferences");

            migrationBuilder.DropTable(
                name: "ParameterSettings");

            migrationBuilder.DropTable(
                name: "PatientReports");

            migrationBuilder.DropTable(
                name: "PythonCodes");

            migrationBuilder.DropTable(
                name: "StudyFinding");

            migrationBuilder.DropTable(
                name: "SystemLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "DicomTags");

            migrationBuilder.DropTable(
                name: "StudyParameters");

            migrationBuilder.DropTable(
                name: "PythonDefaults");

            migrationBuilder.DropTable(
                name: "FindingStructure");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "MeasurementContexts");

            migrationBuilder.DropTable(
                name: "Studies");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
