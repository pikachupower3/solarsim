list = [5, 10, 6, 9, 20]

n = len(list)

for i in range(n):
	for j in range(n-i-1):
    	sorted = True
    	if list[j] > list[j + 1]:
        	list[j + 1], list[j] = list[j], list[j + 1]
            sorted = False
		if sorted:
        	print(list)