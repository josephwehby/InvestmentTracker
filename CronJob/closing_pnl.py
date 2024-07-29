import psycopg2

def connect():
    try:
        conn = psycopg2.connect(
            database="investments",
            user="postgres",
            password="aceofspades",
            host="localhost",
            port=5432
        )
        return conn
    except:
        print("[!] Unable to connect to database")
        return None

def getUsers(cursor):
    cursor.execute("SELECT * FROM users;")
    users = cursor.fetchall()
    users = [list(u) for u in users]
    userids = []
    for user in users:
        userids.append(user[0])
    return userids    

def get_trades(cursor, id):
    cursor.execute("SELECT * FROM trades WHERE userid = %s;", (id,))
    trades = cursor.fetchall()
    return trades

def calculate_pnl(trades):
    # split up trades based on ticker
    trades = [list(t) for t in trades]
    group = {}
    
    for sub in trades:
        ticker = sub[2]
        if ticker not in group:
            group[ticker] = []
        group[ticker].append(sub)

    total_pnl = 0
    for ticker, positions in group.items():
        ticker_pnl = 0
        ticker_current_price = 100
        for p in positions:
            shares = p[4]
            price = p[5]
            pnl = (ticker_current_price - price) * shares
            ticker_pnl += pnl
        total_pnl += ticker_pnl 
    
    return pnl

def updateDatabase(cursor, pnl, id):
    cursor.execute("INSERT INTO historic_pnl (userid, pnl) VALUES (%s, %s)", (id, pnl,))

def main():
    print("[!] Calculating ending day pnl of all users' stock positions...")

    conn = connect()
    
    if conn == None:
        return None
    
    cursor = conn.cursor()
    userids = getUsers(cursor)
    
    for id in userids:
        trades = get_trades(cursor, id)
        pnl = calculate_pnl(trades)
        updateDatabase(cursor, pnl, id)
        print("ID: " + id + " | pnl has been added to historic pnl")
    conn.commit()
    conn.close()

if __name__ == '__main__':
    main()