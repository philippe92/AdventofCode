using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Jour4_2020 : IJour
    {
        List<Passport> passports;

        private class Passport
        {
            public string BirthYear { get; set; }
            public string IssueYear { get; set; }
            public string ExpirationYear { get; set; }
            public string Height { get; set; }
            public string HairColor { get; set; }
            public string EyeColor { get; set; }
            public string PassportId { get; set; }
            public string CountryId { get; set; }

            public bool IsValidFirstPart()
            {
                return !string.IsNullOrEmpty(BirthYear)
                    && !string.IsNullOrEmpty(IssueYear)
                    && !string.IsNullOrEmpty(ExpirationYear)
                    && !string.IsNullOrEmpty(Height)
                    && !string.IsNullOrEmpty(HairColor)
                    && !string.IsNullOrEmpty(EyeColor)
                    && !string.IsNullOrEmpty(PassportId);
            }

            public bool IsValidSecondPart()
            {
                if (!VerifyFormat(BirthYear, @"\d{4}", out _) || !VerifyRange(BirthYear, 1921, 2002))
                {
                    return false;
                }

                if (!VerifyFormat(IssueYear, @"\d{4}", out _) || !VerifyRange(IssueYear, 2010, 2020))
                {
                    return false;
                }

                if (!VerifyFormat(ExpirationYear, @"\d{4}", out _) || !VerifyRange(ExpirationYear, 2020, 2030))
                {
                    return false;
                }

                if (!VerifyFormat(Height, @"(\d+)(\w\w)", out var groups))
                {
                    return false;
                }

                var value = groups[1].Value;
                var unit = groups[2].Value;

                if (unit != "cm" && unit != "in")
                {
                    return false;
                }

                switch (unit)
                {
                    case "cm":
                        if (!VerifyRange(value, 150, 193))
                        {
                            return false;
                        }
                        break;
                    case "in":
                        if (!VerifyRange(value, 59, 76))
                        {
                            return false;
                        }
                        break;
                    default:
                        break;
                }

                if (!VerifyFormat(HairColor, @"#[a-f0-9]{6}", out _))
                {
                    return false;
                }

                if (EyeColor != "amb" && EyeColor != "blu" && EyeColor != "brn" && EyeColor != "gry" && EyeColor != "grn" && EyeColor != "hzl" && EyeColor != "oth")
                {
                    return false;
                }

                if (!VerifyFormat(PassportId, @"[0-9]{9}", out _))
                {
                    return false;
                }

                return true;
            }

            private bool VerifyRange(string s, int low, int high)
            {
                int temp;
                if (!int.TryParse(s, out temp))
                {
                    return false;
                }
                if (temp < low || temp > high)
                {
                    return false;
                }
                return true;
            }

            private bool VerifyFormat(string s, string format, out GroupCollection value)
            {
                var rx = new Regex(format);
                var r = rx.Match(s);
                value = r.Groups;
                return r.Success;
            }

        }

        public void Init(string inputfile)
        {
            string[] lines = File.ReadAllLines(inputfile);
            passports = new List<Passport>();
            Passport currentPassport = new Passport();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passports.Add(currentPassport);
                    currentPassport = new Passport();
                }
                var blocks = line.Split(' ');
                foreach (var block in blocks)
                {
                    var keyval = block.Split(':');
                    switch (keyval[0])
                    {
                        case "byr":
                            currentPassport.BirthYear = keyval[1];
                            break;
                        case "iyr":
                            currentPassport.IssueYear = keyval[1];
                            break;
                        case "eyr":
                            currentPassport.ExpirationYear = keyval[1];
                            break;
                        case "hgt":
                            currentPassport.Height = keyval[1];
                            break;
                        case "hcl":
                            currentPassport.HairColor = keyval[1];
                            break;
                        case "ecl":
                            currentPassport.EyeColor = keyval[1];
                            break;
                        case "pid":
                            currentPassport.PassportId = keyval[1];
                            break;
                        case "cid":
                            currentPassport.CountryId = keyval[1];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void ResolveFirstPart()
        {
            int validcount = 0;
            foreach (var p in passports)
            {
                if (p.IsValidFirstPart())
                {
                    validcount++;
                }  
            }
            Console.WriteLine($"Resultat = {validcount}");
        }

        public void ResolveSecondPart()
        {
            int validcount = 0;
            foreach (var p in passports)
            {
                if (p.IsValidFirstPart() && p.IsValidSecondPart())
                {
                    validcount++;
                }
            }
            Console.WriteLine($"Resultat = {validcount}");
        }
    }
}
