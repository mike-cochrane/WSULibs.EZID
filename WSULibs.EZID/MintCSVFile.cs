using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WSULibs.EZID.Bin
{
	public class MintCSVFile
	{
		public const int LimitAll = -1;

		/// <summary>
		/// Parse a line in CSV format. Handles quoted fields!!!
		/// </summary>
		/// <param name="line">Line to parse</param>
		/// <param name="seperator">Field seperator (default: ,)</param>
		/// <returns></returns>
		public static IList<string> ParseCSVLine(string line, char seperator = ',')
		{
			var results = new List<string>();
			int i = 0, start = 0;
			bool quoted = false;

			// parse the line
			while (i < line.Length)
			{
				if (line[i] == '"')
					quoted = !quoted;

				// if end of string - edge case
				// else easy pie
				if (!quoted  && i == line.Length - 1)
				{
					int s = start + (line[i] == '"' ? 1 : 0);
					int e = i + (line[i] == '"' ? 0 : 1);
					results.Add(line.Substring(s, e - s));
					start = i + 1;
				}
				else if (!quoted && line[i] == seperator)
				{
					// get correct start/end offsets
					int s = start + ((i != 0 && line[i - 1] == '"') ? 1 : 0);
					int e = i - ((i != 0 && line[i - 1] == '"') ? 1 : 0);
					results.Add(line.Substring(s, e - s));
					start = i + 1;
				}

				i++;
			}

			if (quoted)
				throw new Exception("Malformed line");

			return results;
		}

		public delegate Metadata ParseRowFunc(string line);

		public string Shoulder { get; set; }

		public string CsvFilePath { get; set; }

		public Authentication Authentication { get; set; }

		public int Limit { get; set; }

		public string ResultFile { get; set; }

		public MintCSVFile(string shoulder = null, string csvFilePath = null, string resultFile = null, Authentication authentication = null, int limit = LimitAll)
		{
			this.Shoulder = shoulder;
			this.CsvFilePath = csvFilePath;
			this.ResultFile = resultFile;
			this.Authentication = authentication;
			this.Limit = limit;
		}

		public void Execute(ParseRowFunc parserFunc)
		{
			// validate parameters
			if (String.IsNullOrWhiteSpace(this.Shoulder))
				throw new NullReferenceException("Shoulder must not be null");

			if (String.IsNullOrWhiteSpace(this.CsvFilePath))
				throw new NullReferenceException("CSVFilePath must not be null");

			if (this.Authentication == null)
				throw new NullReferenceException("Authentication must not be null");

			// open csv file for reading
			using (var csvFileStream = File.Open(this.CsvFilePath, FileMode.Open, FileAccess.Read))
			{
				var csvFileReader = new StreamReader(csvFileStream, UTF8Encoding.UTF8, true);

				// open result file for writing
				var resultFile = this.ResultFile;
				if(resultFile == null)
					resultFile = String.Format("{0}.result.csv", Path.GetFileNameWithoutExtension(this.CsvFilePath));

				using (var resultFileStream = File.Open(resultFile, FileMode.Append, FileAccess.Write))
				{
					var resultFileWriter = new StreamWriter(resultFileStream, UTF8Encoding.UTF8);

					// read all lines for until line limit it hit
					int count = 0;
					while (!csvFileReader.EndOfStream && (count < this.Limit || this.Limit == LimitAll))
					{
						string line = null;
						try
						{
							// parse the line
							line = csvFileReader.ReadLine();
							var metadata = parserFunc(line);
							
							// make the request
							var request = new MintRequest(this.Shoulder, metadata, this.Authentication);
							var response = request.ExecuteRequest();

							// ensure the response is valid
							if (String.IsNullOrWhiteSpace(response))
								throw new Exception("No identifier returned");

							// write response to result file
							resultFileWriter.WriteLine(String.Format("{0},{1}", line, response));
						}
						catch (Exception e)
						{
							// something happened, track it in result file
							if (line == null)
							{
								resultFileWriter.WriteLine(String.Format(e.Message));
							}
							else
							{
								resultFileWriter.WriteLine(String.Format("{0},{1}", line, e.Message));
							}
						}
						finally
						{
							count++;
						}
					}

					csvFileReader.Close();
					resultFileWriter.Close();
				}
			}
		}
	}
}
