import requests

api = "Token "

header = {
    'Content-Type': 'application/json',
    'Authorization': api
}

r = requests.get("https://api.tiingo.com/iex/SPY", headers=header)
data = r.json()
print(data[0]["last"])
#print(data["last"])
