#python csv level parser

def main():

        #GET THE FILE NAME FROM THE USER
        #THE FILE MUST BE IN THE SAME FOLDER
	print("Filename: ")
	file = raw_input()

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

	#PUT ALL THE STUFF IN IT
	for item in a:
		testtxt.write("%s\n" % (item,))

	#MAKE SURE TO CLOSE THE FILE!!!
	testtxt.close()

if __name__ == '__main__':
    main()
