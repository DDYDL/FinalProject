import sys
import logging
import pymysql
import dbinfo
import json
import random

item = 0 # 전역변수

connection = pymysql.connect(host = dbinfo.db_host, port = dbinfo.db_port,
    user = dbinfo.db_username, passwd = dbinfo.db_password, db = dbinfo.db_name)

def lambda_handler(event, context):
    # event['body'] in   command=input&label=labelInput&coordi=coordiInput&info=infoInput
    
    for a in range(0, len(event['coordi'])):
        
        count = len(event['coordi'])
        
        membercode = event['coordi'][int(a)]['memberCode']
        placecode = event['coordi'][int(a)]['placeCode']
        spacename = event['coordi'][int(a)]['spaceName']
        roomname = event['coordi'][int(a)]['roomName']
        coordi = event['coordi'][int(a)]['coordi']
        latitude = event['coordi'][int(a)]['latitude']
        longitude = event['coordi'][int(a)]['longitude']
        altitude = event['coordi'][int(a)]['altitude']
        direction = event['coordi'][int(a)]['direction']
        #membercode = json.loads(membercode)
        #placecode = json.loads(placecode)
        #coordi = json.loads(coordi)
        
        lambda_xyz(membercode, placecode, spacename, roomname, coordi, latitude, longitude, altitude, direction, a, count)
        

    return {
            'statusCode': 200,
            'body': "Register complete"
        }


def lambda_xyz(membercode, placecode, spacename, roomname, coordi, latitude, longitude, altitude, direction, a, count):
    
    query = f"select * from Place where BINARY MemberCode = '{membercode}' and BINARY PlaceCode = '{placecode}' and BINARY Coordi = '{coordi}'"
    # 3가지 모두 겹치는 행이 있으면(rows가 0초과) 안 넣음
    cursor = connection.cursor()
    cursor.execute(query)
    rows = cursor.fetchall()
    
    queryObject = f"select * from Model where BINARY Type = 'NPC'"
    # Model 테이블에서 Type이 NPC인 행을 모두 가져옴
    cursorObject = connection.cursor()
    cursorObject.execute(queryObject)
    rowsObject = cursorObject.fetchall()
    
    #membercode = int(membercode)
    #placecode = int(placecode)
    
    if len(rows) == 0:
         # 랜덤으로 모델 배정
        qul = f"select ModelCode from Model"
        cursor.execute(qul)
        rows = cursor.fetchall()
        
        global item # 전역변수로 선언
        
        if a < 4:
            i = random.randrange(1, 4) # 1~3사이의 Attack NPC을 2명 뽑음
            if a >= 2:
                i = 12 # Attack NPC를 없앨 도구인 부적을 2개 뽑음
        elif a < 6:
            if a < 5:
                i = random.randrange(4, 7) # 4~6사이의 NoAttack NPC을 1명 뽑음
                item = i
            if a >= 5:
                i = item + 9 # NoAttack NPC와 맞는 도구를 뽑기 위해 이전 i 값에 9를 더해 도구를 1개 뽑음
        elif a < 8:
            i = random.randrange(7, 10) # 7~9사이의 Pop NPC을 2명 뽑음
        # NPC 5명, Item 3개 뽑음
        
        elif a < 11:
            i = 0 # 보스 뽑음
            if a >= 9:
                i = 17 # 보스 아이템 뽑음
            if a >= 10:
                i = 18 # 보스 아이템 뽑음
        # 11개 뽑음
        
        #elif a > 10 and a < (count - int((count-11)/2)):
        #    i = 10 # 남은 개수의 절반은 쪽지, 절반은 식혜로 할당
        
        elif a < 16:
            i = 10
        else:
            i = 11
        
         
        #i = len(rowsObject) + a # 현재 NPC수 10 + a
        #if i >= len(rows):
            #i = random.randrange(0, len(rowsObject))
        
        sql = "INSERT INTO Place (MemberCode, PlaceCode, SpaceName, RoomName, Coordi, ModelCode, Latitude, Longitude, Altitude, Direction) VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"
        val = (membercode, placecode, spacename, roomname, coordi, i, latitude, longitude, altitude, direction)
        cursor.execute(sql, val)
        connection.commit()
        
       
        
        return {
            'statusCode': 200,
            'body': json.dumps("Register complete")
        }
    else:
        return {
            'statusCode': 400,
            'body': "Fail to register" 
        }
        
    
    
def lambda_login(label, coordi):

    query = f"select * from UnityTable where BINARY coordi = '{coordi}'"
    cursor = connection.cursor()
    cursor.execute(query)
    rows = cursor.fetchall()
  
    if len(rows) == 0:
        return {
            'statusCode': 400,
            'body': "Fail to input" 
        }
    else:
        return {
            'statusCode': 200,
            'body': rows[0][2]
        }