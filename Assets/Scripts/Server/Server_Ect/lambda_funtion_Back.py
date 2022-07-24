import sys
import logging
import pymysql
import dbinfo
import json

connection = pymysql.connect(host = dbinfo.db_host, port = dbinfo.db_port,
    user = dbinfo.db_username, passwd = dbinfo.db_password, db = dbinfo.db_name)

def lambda_handler(event, context):
    # event['body'] in   command=input&label=labelInput&coordi=coordiInput&info=infoInput

    command = event['command']
    membercode = event['code'].split('&')[0]
    placecode = event['code'].split('&')[1]
    
    if command == 'info':
        return lambda_back(membercode, placecode)
    #if command == 'init':
    #    return lambda_init()
    else:
        return {
            'statusCode': 400,
            'body': "Invalid command" 
        }
        

def lambda_back(membercode, placecode):
    
    query = f"select Coordi, ModelCode from Place where BINARY MemberCode = '{membercode}' and BINARY PlaceCode = '{placecode}'"
    cursor = connection.cursor()
    cursor.execute(query)
    rows = cursor.fetchall()
    connection.commit()

    # rows 형식 {"body": [["1,2,3",2],["4,5,6",1]]}
    
    return {
        'body': rows
    }
    
    
# def lambda_init(membercode, placecode):