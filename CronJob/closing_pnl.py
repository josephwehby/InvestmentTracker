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

def get_trades(cursor):
    cursor.execute("SELECT * FROM trades;")
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
    print(group)

    total_pnl = 0
    for ticker, positions in group.items():
        ticker_pnl = 0
        ticker_current_price = 100
        for p in positions:
            shares = p[3]
            price = p[4]
            pnl = (ticker_current_price - price) * shares
            ticker_pnl += pnl
        total_pnl += ticker_pnl 
    
    return pnl

def updateDatabase(cursor, pnl):
    cursor.execute("INSERT INTO historic_pnl (pnl) VALUES (%s)", (pnl,))

def main():
    print("[!] Calculating ending day pnl of all stock positions...")

    conn = connect()
    
    if conn == None:
        return None
    
    cursor = conn.cursor()
    trades = get_trades(cursor)
    pnl = calculate_pnl(trades)
    updateDatabase(cursor, pnl)
    conn.commit()
    conn.close()

if __name__ == '__main__':
    main()