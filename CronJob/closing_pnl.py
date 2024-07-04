import psycopg2


CLOSING_PRICE = 100

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
    pnl = 0
    return pnl

def main():
    print("[!] Calculating ending day pnl of all stock positions...")

    conn = connect()
    
    if conn == None:
        return None
    
    cursor = conn.cursor()
    get_trades(cursor)

    conn.close()

if __name__ == '__main__':
    main()