-- City -> Password rename,  Customer 입력을 위해 Password를 추가(rename)했습니다.

여기에 있는 Customer table 스크립트로 생성하지 않고 Northwind DB로 생성한 경우 아래 명령어로
필드명을 수정하세요

SP_RENAME 'Customers.[City]','Password','COLUMN'

#https://github.com/rubyonsoft/DotNetLecture/blob/main/Update_DBScript
