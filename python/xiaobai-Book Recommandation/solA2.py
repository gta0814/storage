
def openAccount(customerDB, numBooks):
    uid = input("Please enter a username: ")
    while uid in customerDB: 
        print("The username", uid, "is already in use.")
        uid = input("Please try a different name: ")
    customerDB[uid] = [0]*numBooks
    print("Account created successfully. Log in to use this account.")

def login(customerDB):
    uid = input("Please enter your username: ")
    while uid not in customerDB: 
        tryagain = input("Customer not found. Would you like to try again (y/n): ")
        if tryagain.lower() != "y": 
            print("Login Failed.")
            return None
        else: 
            uid = input("Please enter your username: ")
    print("Login Succeeded.")
    print("Welcome back,", uid)
    return uid

def queryRating(currentScore):
    SCOREMAP = [0, -5, -3, 1, 3, 5]
    newScore = None
    if currentScore == 0: print("\n************\nYou have not rated this book")
    else: print("\n************\nYou have rated this book", SCOREMAP.index(currentScore))
    while True: 
        if currentScore == 0: newScore = input("Please enter a score (1-5), enter to leave unscored, or q to quit:")
        else: newScore = input("Please enter a new score (1-5), d to delete your score, enter to leave it unchanged, or q to quit:")
            
        if newScore == "": return currentScore
        elif newScore == "d": return 0
        elif newScore == "q": return newScore
        elif newScore in ["1", "2", "3", "4", "5"]: return SCOREMAP[int(newScore)]
        else: print("Invalid Input")

def rateBooks(customer, customerDB, bookDB):
    if customer not in customerDB: 
        print("Customer Not Found")
        return
    for bkYear, bkTitle, bkAuthor, bkIndex in bookDB.values(): 
        print(bkTitle, "by", bkAuthor)
        currentRating = customerDB[customer][bkIndex]
        newScore = queryRating(currentRating)
        if newScore == "q": return
        else: customerDB[customer][bkIndex] = newScore
        print("")

def getOneBooksRatings (customerDB, bookIndex):
    ratings = []
    for customerRatings in customerDB.values():
        r = customerRatings[bookIndex]
        if r != 0: ratings.append(r)
    return ratings

def getAverageRatings(customerDB, bookDB): 
    averageRatings = []    
    for isbn, book in bookDB.items(): 
        bkRatings = getOneBooksRatings(customerDB, book[3])
        if len(bkRatings)>0: averageRatings.append((sum(bkRatings)/len(bkRatings), isbn))
    averageRatings.sort()
    averageRatings.reverse()
    return averageRatings

def topThree(customerDB, bookDB): 
    averageRatings = getAverageRatings(customerDB, bookDB)
    print("The three most highly rated books in our database are:")
    for bkTuple in averageRatings[:3]: 
        bkData = bookDB[bkTuple[1]]
        print("  ", bkData[1], "by", bkData[2], "with a score of", round(bkTuple[0], 2))

def basicRecommendation(customer, customerDB, bookDB):    
    averageRatings = getAverageRatings(customerDB, bookDB)
    for score,isbn in averageRatings: 
        bookIndex = bookDB[isbn][3]
        if customerDB[customer][bookIndex] == 0: 
            print("You might like:")
            print("  ", bookDB[isbn][1], "by", bookDB[isbn][2], "with a score of", round(score, 2))
            return
    print("Sorry! We have nothing to recommend. It looks like you've read all our books!")
