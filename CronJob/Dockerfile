FROM python:3.12-slim 

WORKDIR /app 

COPY . /app 

RUN apt-get update && apt-get install -y cron tzdata

RUN pip install -r requirements.txt 

COPY historicpnl_cron /etc/cron.d/pnl_cron
RUN chmod 0644 /etc/cron.d/pnl_cron 

RUN crontab /etc/cron.d/pnl_cron
RUN touch /var/log/cron.log
RUN chmod 0666 /var/log/cron.log

CMD ["cron", "-f"]