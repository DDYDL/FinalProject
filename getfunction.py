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
    if command == 'init':
        return lambda_init(membercode)
    else:
        return {
            'statusCode': 400,
            'body': "Invalid command" 
        }
        

def lambda_back(membercode, placecode):
    
    query = f"select Coordi, ModelCode, Latitude, Longitude, Altitude, Direction from Place where\
    BINARY MemberCode = '{membercode}' and BINARY PlaceCode = '{placecode}'"
    cursor = connection.cursor()
    cursor.execute(query)
    rows = cursor.fetchall()
    connection.commit()

    # rows 형식 {"body": [["1,2,3",2,0,0,0,0],["4,5,6",1,0,0,0,0]]}
    
    return {
        'body': rows
    }
    
    
def lambda_init(membercode):
    query = f"select PlaceCode, SpaceName from Place where BINARY MemberCode = '{membercode}'"
    cursor = connection.cursor()
    cursor.execute(query)
    rows = cursor.fetchall()
    connection.commit()
    
    if len(rows) != 0:
        row = rows[0]
        ls = [] # 균일하게 값이 들어가도록 선언 후 append 로 추
        ls.append(list(row)) # 리스트로 변환, 튜플은 추가 불가
    
        for a in range(1, len(rows)):
            if (rows[a-1][0] != rows[a][0]): # 중복 제거 후 추가
                ls.append(rows[a])
    
        row = tuple(ls) # 다시 튜플로 변환
                
        return {
            'body': ls
        }