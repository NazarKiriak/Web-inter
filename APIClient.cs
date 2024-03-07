using System;
using System.Net.Http;
using System.Threading.Tasks;

public class APIClient
{
	private readonly HttpClient _httpClient;

	public APIClient()
	{
		_httpClient = new HttpClient();
	}

	public async Task<ResponseModel<string>> GetRandomText()
	{
		string apiUrl = "https://www.googleapis.com/customsearch/v1?key=AIzaSyC3bqewtY812fSX2rgyqcJAwW2T2kBTx8s&cx=017576662512468239146:omuauf_lfve&q=lectures";
		return await Get(apiUrl);
	}
	public async Task<ResponseModel<string>> Get(string url)
	{
		var responseModel = new ResponseModel<string>();
		try
		{
			HttpResponseMessage response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			responseModel.Message = "HTTP request successful.";
			responseModel.HttpStatusCode = (int)response.StatusCode;
			responseModel.Data = new List<string> { responseBody };
		}
		catch (HttpRequestException e)
		{
			responseModel.Message = $"HTTP request failed: {e.Message}";
			responseModel.HttpStatusCode = 500; // Internal Server Error
			responseModel.Data = null;
		}
		return responseModel;
	}

	public async Task<ResponseModel<string>> Post(string url, string data)
	{
		var responseModel = new ResponseModel<string>();
		try
		{
			var content = new StringContent(data);
			HttpResponseMessage response = await _httpClient.PostAsync(url, content);
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			responseModel.Message = "HTTP request successful.";
			responseModel.HttpStatusCode = (int)response.StatusCode;
			responseModel.Data = new List<string> { responseBody };
		}
		catch (HttpRequestException e)
		{
			responseModel.Message = $"HTTP request failed: {e.Message}";
			responseModel.HttpStatusCode = 500; // Internal Server Error
			responseModel.Data = null;
		}
		return responseModel;
	}
}
