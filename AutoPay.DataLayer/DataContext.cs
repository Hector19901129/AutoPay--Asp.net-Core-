﻿using AutoPay.DataLayer.EntityConfigurations;
using AutoPay.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoPay.DataLayer
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new RemoteDbConfigConfiguration());
            builder.ApplyConfiguration(new BatchConfiguration());
            builder.ApplyConfiguration(new BatchCustomerConfiguration());
            builder.ApplyConfiguration(new BatchCustomerDueDetailConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new PaymentConfiguration());
            builder.ApplyConfiguration(new PaymentErrorConfiguration());

            SeedData(builder);
        }

        private static void SeedData(ModelBuilder builder)
        {
            #region seed contest type

            builder.Entity<Country>().HasData(new Country { Id = 1, Code = "AD", Name = "Andorra", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 2, Code = "AE", Name = "United Arab Emirates", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 3, Code = "AF", Name = "Afghanistan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 4, Code = "AG", Name = "Antigua and Barbuda", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 5, Code = "AI", Name = "Anguilla", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 6, Code = "AL", Name = "Albania", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 7, Code = "AM", Name = "Armenia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 8, Code = "AN", Name = "Netherlands Antilles", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 9, Code = "AO", Name = "Angola", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 10, Code = "AQ", Name = "Antarctica", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 11, Code = "AR", Name = "Argentina", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 12, Code = "AS", Name = "American Samoa", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 13, Code = "AT", Name = "Austria", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 14, Code = "AU", Name = "Australia", Order = 0 });
            builder.Entity<Country>().HasData(new Country { Id = 15, Code = "AW", Name = "Aruba", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 16, Code = "AX", Name = "Aland Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 17, Code = "AZ", Name = "Azerbaijan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 18, Code = "BA", Name = "Bosnia and Herzegovina", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 19, Code = "BB", Name = "Barbados", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 20, Code = "BD", Name = "Bangladesh", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 21, Code = "BE", Name = "Belgium", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 22, Code = "BF", Name = "Burkina Faso", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 23, Code = "BG", Name = "Bulgaria", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 24, Code = "BH", Name = "Bahrain", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 25, Code = "BI", Name = "Burundi", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 26, Code = "BJ", Name = "Benin", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 27, Code = "BM", Name = "Bermuda", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 28, Code = "BN", Name = "Brunei Darussalam", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 29, Code = "BO", Name = "Bolivia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 30, Code = "BR", Name = "Brazil", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 31, Code = "BS", Name = "Bahamas", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 32, Code = "BT", Name = "Bhutan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 33, Code = "BV", Name = "Bouvet Island", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 34, Code = "BW", Name = "Botswana", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 35, Code = "BY", Name = "Belarus", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 36, Code = "BZ", Name = "Belize", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 37, Code = "CA", Name = "Canada", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 38, Code = "CC", Name = "Cocos (Keeling) Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 39, Code = "CD", Name = "Democratic Republic of the Congo", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 40, Code = "CF", Name = "Central African Republic", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 41, Code = "CG", Name = "Congo", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 42, Code = "CH", Name = "Switzerland", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 43, Code = "CI", Name = "Cote D'Ivoire(Ivory Coast)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 44, Code = "CK", Name = "Cook Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 45, Code = "CL", Name = "Chile", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 46, Code = "CM", Name = "Cameroon", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 47, Code = "CN", Name = "China", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 48, Code = "CO", Name = "Colombia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 49, Code = "CR", Name = "Costa Rica", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 50, Code = "CS", Name = "Serbia and Montenegro", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 51, Code = "CU", Name = "Cuba", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 52, Code = "CV", Name = "Cape Verde", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 53, Code = "CX", Name = "Christmas Island", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 54, Code = "CY", Name = "Cyprus", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 55, Code = "CZ", Name = "Czech Republic", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 56, Code = "DE", Name = "Germany", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 57, Code = "DJ", Name = "Djibouti", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 58, Code = "DK", Name = "Denmark", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 59, Code = "DM", Name = "Dominica", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 60, Code = "DO", Name = "Dominican Republic", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 61, Code = "DZ", Name = "Algeria", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 62, Code = "EC", Name = "Ecuador", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 63, Code = "EE", Name = "Estonia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 64, Code = "EG", Name = "Egypt", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 65, Code = "EH", Name = "Western Sahara", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 66, Code = "ER", Name = "Eritrea", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 67, Code = "ES", Name = "Spain", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 68, Code = "ET", Name = "Ethiopia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 69, Code = "FI", Name = "Finland", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 70, Code = "FJ", Name = "Fiji", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 71, Code = "FK", Name = "Falkland Islands (Malvinas)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 72, Code = "FM", Name = "Federated States of Micronesia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 73, Code = "FO", Name = "Faroe Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 74, Code = "FR", Name = "France", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 75, Code = "FX", Name = "France, Metropolitan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 76, Code = "GA", Name = "Gabon", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 77, Code = "GB", Name = "Great Britain (UK)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 78, Code = "GD", Name = "Grenada", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 79, Code = "GE", Name = "Georgia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 80, Code = "GF", Name = "French Guiana", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 81, Code = "GH", Name = "Ghana", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 82, Code = "GI", Name = "Gibraltar", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 83, Code = "GL", Name = "Greenland", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 84, Code = "GM", Name = "Gambia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 85, Code = "GN", Name = "Guinea", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 86, Code = "GP", Name = "Guadeloupe", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 87, Code = "GQ", Name = "Equatorial Guinea", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 88, Code = "GR", Name = "Greece", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 89, Code = "GS", Name = "S. Georgia and S. Sandwich Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 90, Code = "GT", Name = "Guatemala", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 91, Code = "GU", Name = "Guam", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 92, Code = "GW", Name = "Guinea-Bissau", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 93, Code = "GY", Name = "Guyana", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 94, Code = "HK", Name = "Hong Kong", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 95, Code = "HM", Name = "Heard Island and McDonald Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 96, Code = "HN", Name = "Honduras", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 97, Code = "HR", Name = "Croatia (Hrvatska)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 98, Code = "HT", Name = "Haiti", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 99, Code = "HU", Name = "Hungary", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 100, Code = "ID", Name = "Indonesia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 101, Code = "IE", Name = "Ireland", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 102, Code = "IL", Name = "Israel", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 103, Code = "IN", Name = "India", Order = 0 });
            builder.Entity<Country>().HasData(new Country { Id = 104, Code = "IO", Name = "British Indian Ocean Territory", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 105, Code = "IQ", Name = "Iraq", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 106, Code = "IR", Name = "Iran", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 107, Code = "IS", Name = "Iceland", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 108, Code = "IT", Name = "Italy", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 109, Code = "JM", Name = "Jamaica", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 110, Code = "JO", Name = "Jordan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 111, Code = "JP", Name = "Japan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 112, Code = "KE", Name = "Kenya", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 113, Code = "KG", Name = "Kyrgyzstan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 114, Code = "KH", Name = "Cambodia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 115, Code = "KI", Name = "Kiribati", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 116, Code = "KM", Name = "Comoros", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 117, Code = "KN", Name = "Saint Kitts and Nevis", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 118, Code = "KP", Name = "Korea (North)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 119, Code = "KR", Name = "Korea (South)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 120, Code = "KW", Name = "Kuwait", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 121, Code = "KY", Name = "Cayman Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 122, Code = "KZ", Name = "Kazakhstan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 123, Code = "LA", Name = "Laos", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 124, Code = "LB", Name = "Lebanon", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 125, Code = "LC", Name = "Saint Lucia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 126, Code = "LI", Name = "Liechtenstein", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 127, Code = "LK", Name = "Sri Lanka", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 128, Code = "LR", Name = "Liberia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 129, Code = "LS", Name = "Lesotho", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 130, Code = "LT", Name = "Lithuania", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 131, Code = "LU", Name = "Luxembourg", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 132, Code = "LV", Name = "Latvia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 133, Code = "LY", Name = "Libya", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 134, Code = "MA", Name = "Morocco", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 135, Code = "MC", Name = "Monaco", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 136, Code = "MD", Name = "Moldova", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 137, Code = "MG", Name = "Madagascar", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 138, Code = "MH", Name = "Marshall Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 139, Code = "MK", Name = "Macedonia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 140, Code = "ML", Name = "Mali", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 141, Code = "MM", Name = "Myanmar", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 142, Code = "MN", Name = "Mongolia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 143, Code = "MO", Name = "Macao", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 144, Code = "MP", Name = "Northern Mariana Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 145, Code = "MQ", Name = "Martinique", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 146, Code = "MR", Name = "Mauritania", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 147, Code = "MS", Name = "Montserrat", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 148, Code = "MT", Name = "Malta", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 149, Code = "MU", Name = "Mauritius", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 150, Code = "MV", Name = "Maldives", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 151, Code = "MW", Name = "Malawi", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 152, Code = "MX", Name = "Mexico", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 153, Code = "MY", Name = "Malaysia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 154, Code = "MZ", Name = "Mozambique", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 155, Code = "NA", Name = "Namibia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 156, Code = "NC", Name = "New Caledonia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 157, Code = "NE", Name = "Niger", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 158, Code = "NF", Name = "Norfolk Island", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 159, Code = "NG", Name = "Nigeria", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 160, Code = "NI", Name = "Nicaragua", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 161, Code = "NL", Name = "Netherlands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 162, Code = "NO", Name = "Norway", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 163, Code = "NP", Name = "Nepal", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 164, Code = "NR", Name = "Nauru", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 165, Code = "NU", Name = "Niue", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 166, Code = "NZ", Name = "New Zealand", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 167, Code = "OM", Name = "Oman", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 168, Code = "PA", Name = "Panama", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 169, Code = "PE", Name = "Peru", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 170, Code = "PF", Name = "French Polynesia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 171, Code = "PG", Name = "Papua New Guinea", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 172, Code = "PH", Name = "Philippines", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 173, Code = "PK", Name = "Pakistan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 174, Code = "PL", Name = "Poland", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 175, Code = "PM", Name = "Saint Pierre and Miquelon", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 176, Code = "PN", Name = "Pitcairn", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 177, Code = "PR", Name = "Puerto Rico", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 178, Code = "PS", Name = "Palestinian Territory", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 179, Code = "PT", Name = "Portugal", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 180, Code = "PW", Name = "Palau", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 181, Code = "PY", Name = "Paraguay", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 182, Code = "QA", Name = "Qatar", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 183, Code = "RE", Name = "Reunion", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 184, Code = "RO", Name = "Romania", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 185, Code = "RU", Name = "Russian Federation", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 186, Code = "RW", Name = "Rwanda", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 187, Code = "SA", Name = "Saudi Arabia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 188, Code = "SB", Name = "Solomon Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 189, Code = "SC", Name = "Seychelles", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 190, Code = "SD", Name = "Sudan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 191, Code = "SE", Name = "Sweden", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 192, Code = "SG", Name = "Singapore", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 193, Code = "SH", Name = "Saint Helena", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 194, Code = "SI", Name = "Slovenia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 195, Code = "SJ", Name = "Svalbard and Jan Mayen", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 196, Code = "SK", Name = "Slovakia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 197, Code = "SL", Name = "Sierra Leone", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 198, Code = "SM", Name = "San Marino", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 199, Code = "SN", Name = "Senegal", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 200, Code = "SO", Name = "Somalia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 201, Code = "SR", Name = "Suriname", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 202, Code = "ST", Name = "Sao Tome and Principe", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 203, Code = "SU", Name = "USSR (former)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 204, Code = "SV", Name = "El Salvador", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 205, Code = "SY", Name = "Syria", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 206, Code = "SZ", Name = "Swaziland", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 207, Code = "TC", Name = "Turks and Caicos Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 208, Code = "TD", Name = "Chad", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 209, Code = "TF", Name = "French Southern Territories", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 210, Code = "TG", Name = "Togo", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 211, Code = "TH", Name = "Thailand", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 212, Code = "TJ", Name = "Tajikistan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 213, Code = "TK", Name = "Tokelau", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 214, Code = "TL", Name = "Timor-Leste", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 215, Code = "TM", Name = "Turkmenistan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 216, Code = "TN", Name = "Tunisia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 217, Code = "TO", Name = "Tonga", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 218, Code = "TP", Name = "East Timor", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 219, Code = "TR", Name = "Turkey", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 220, Code = "TT", Name = "Trinidad and Tobago", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 221, Code = "TV", Name = "Tuvalu", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 222, Code = "TW", Name = "Taiwan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 223, Code = "TZ", Name = "Tanzania", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 224, Code = "UA", Name = "Ukraine", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 225, Code = "UG", Name = "Uganda", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 226, Code = "UK", Name = "United Kingdom", Order = 0 });
            builder.Entity<Country>().HasData(new Country { Id = 227, Code = "UM", Name = "United States Minor Outlying Islands", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 228, Code = "US", Name = "United States", Order = 0 });
            builder.Entity<Country>().HasData(new Country { Id = 229, Code = "UY", Name = "Uruguay", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 230, Code = "UZ", Name = "Uzbekistan", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 231, Code = "VA", Name = "Vatican City State (Holy See)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 232, Code = "VC", Name = "Saint Vincent and the Grenadines", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 233, Code = "VE", Name = "Venezuela", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 234, Code = "VG", Name = "Virgin Islands (British)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 235, Code = "VI", Name = "Virgin Islands (U.S.)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 236, Code = "VN", Name = "Viet Nam", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 237, Code = "VU", Name = "Vanuatu", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 238, Code = "WF", Name = "Wallis and Futuna", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 239, Code = "WS", Name = "Samoa", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 240, Code = "YE", Name = "Yemen", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 241, Code = "YT", Name = "Mayotte", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 242, Code = "YU", Name = "Yugoslavia (former)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 243, Code = "ZA", Name = "South Africa", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 244, Code = "ZM", Name = "Zambia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 245, Code = "ZR", Name = "Zaire (former)", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 246, Code = "ZW", Name = "Zimbabwe", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 251, Code = "CH", Name = "Switzerland", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 252, Code = "CH", Name = "Switzerland", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 267, Code = "NO", Name = "Norway", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 269, Code = "SP", Name = "Serbia", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 275, Code = "ZZ", Name = "Uknown", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 276, Code = "IM", Name = "Isle of Man", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 277, Code = "AP", Name = "Asia/Pacific Region", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 278, Code = "GE", Name = "Guernsey", Order = 1 });
            builder.Entity<Country>().HasData(new Country { Id = 279, Code = "AD", Name = "Andorra", Order = 1 });

            #endregion
        }
    }
}
