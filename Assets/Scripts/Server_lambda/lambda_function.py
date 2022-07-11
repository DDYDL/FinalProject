import sys
import logging
import pymysql
import dbinfo
import json
import random

connection = pymysql.connect(host = dbinfo.db_host, port = dbinfo.db_port,
    user = dbinfo.db_username, passwd = dbinfo.db_password, db = dbinfo.db_name)

def lambda_handler(event, context):
    # event['body'] in   command=input&label=labelInput&coordi=coordiInput&info=infoInput
    
    for a in range(0, len(event['coordi'])):
        membercode = event['coordi'][int(a)]['memberCode']
        placecode = event['coordi'][int(a)]['placeCode']
        coordi = event['coordi'][int(a)]['coordi']
        #membercode = json.loads(membercode)
        #placecode = json.loads(placecode)
        #coordi = json.loads(coordi)
        lambda_xyz(membercode, placecode, coordi)
        

    return {
            'statusCode': 200,
            'body': "Register complete"
        }


def lambda_xyz(membercode, placecode, coordi):
    
    query = f"select * from Place where BINARY MemberCode = '{membercode}' and BINARY PlaceCode = '{placecode}' and BINARY Coordi = '{coordi}'" # 3가지 모두 겹치면 안 넣음
    cursor = connection.cursor()
    cursor.execute(query)
    rows = cursor.fetchall()
    
    #membercode = int(membercode)
    #placecode = int(placecode)
    
    if len(rows) == 0:
         # 랜덤으로 모델 배정
        qul = f"select ModelCode from Model"
        cursor.execute(qul)
        rows = cursor.fetchall()
        i = random.randrange(1, len(rows)+1)
        
        sql = "INSERT INTO Place (MemberCode, PlaceCode, Coordi, ModelCode) VALUES (%s, %s, %s, %s)"
        val = (membercode, placecode, coordi, i)
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
        
        
        
def lambda_input(label, coordi):

    query = f"select * from UnityTable where BINARY label = '{label}' and BINARY coordi = '{coordi}'"
    cursor = connection.cursor()
    cursor.execute(query)
    rows = cursor.fetchall()
  
    if len(rows) == 0:
        sql = "INSERT INTO UnityTable (label, coordi) VALUES (%s, %s)"
        val = (label, coordi)
        cursor.execute(sql, val)
        connection.commit()
        return {
            'statusCode': 200,
            'body': "Register complete" 
        }
    else:
        return {
            'statusCode': 400,
            'body': "Fail to register" 
        }
        
        
        
def lambda_save(label, coordi, info):

    query = f"select * from UnityTable where BINARY label = '{label}' and BINARY coordi = '{coordi}'"
    cursor = connection.cursor()
    cursor.execute(query)
    rows = cursor.fetchall()

    query = f"update UnityTable set info = '{info}' where BINARY label = '{label}' and BINARY coordi = '{coordi}'"
    cursor = connection.cursor()
    cursor.execute(query)
    connection.commit()
    
    if len(rows) == 0:
        return {
                'statusCode': 400,
                'body': "Save fail" 
            }
    else:
        return {
                'statusCode': 200,
                'body': "Save Complete" 
            }
