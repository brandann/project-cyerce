#python csv level parser

def parse_game(name):

	file = name + "_Game.csv"

	#READ ALL THE CSV LINES TO THE LIST
	with open(file) as f:
		lines = f.readlines()
	
        #CLEAN ALL THE LINES AND ADD {} TO THE LINES
	a = []
	for line in lines:
		a.append("{" + line.strip() + "},")

	#ON THE LAST LINE, REMOVE THE TRAILING COMMA
	a[len(a) - 1] = "{" + lines[len(lines) - 1] + "}"

	#CREATE A NEW FILE WITH THE SAME NAME AS A TXT TYPE
	testtxt = open(file.replace(".csv",".txt"),"w")

	testtxt.write("int[,] intmap = new int[,] {\n")
	
	#PUT ALL THE STUFF IN IT
	for item in a:
		testtxt.write("%s\n" % (item,))

	testtxt.write("};")
        
	#MAKE SURE TO CLOSE THE FILE!!!
	testtxt.close()

	#TELL THE USER IT WORKED!
	print("CREATED: Game File: " + file)
	
def parse_back(name):

	file = name + "_Back.csv"

	#READ ALL THE CSV LINES TO THE LIST
	with open(file) as f:
		lines = f.readlines()
	
        #CLEAN ALL THE LINES AND ADD {} TO THE LINES
	a = []
	for line in lines:
		a.append("{" + line.strip() + "},")

	#ON THE LAST LINE, REMOVE THE TRAILING COMMA
	a[len(a) - 1] = "{" + lines[len(lines) - 1] + "}"

	#CREATE A NEW FILE WITH THE SAME NAME AS A TXT TYPE
	testtxt = open(file.replace(".csv",".txt"),"w")

	testtxt.write("int[,] intbackground = new int[,] {\n")
	
	#PUT ALL THE STUFF IN IT
	for item in a:
		testtxt.write("%s\n" % (item,))

	testtxt.write("};")

	#MAKE SURE TO CLOSE THE FILE!!!
	testtxt.close()

	#TELL THE USER IT WORKED!
	print("CREATED: Back File: " + file)

def main():
        print("Enter Map Level (Mapxxx)")
        file = input()
        parse_game(file)
        parse_back(file)
        print("\n---------------------------------------------------\n")
        
if __name__ == '__main__':
        while(True):
            main()
