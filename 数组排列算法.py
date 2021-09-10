import copy
import random
from collections import deque
from timeit import default_timer as timer
from multiprocessing import Process
import time

def itterativeDeepening():
    global open_list
    global closed_list
    depth_level =1
    done = None
    loop_again = True
    original_open_list = copy.deepcopy(open_list)


    while loop_again:

        open_list = copy.deepcopy(original_open_list)
        closed_list.clear()

        loop_again = False

        while len(open_list) > 0 :

            current_state = open_list.pop()

            #if the depth of the node > the maximum allowed depth for this iteration, check the next state in the open list
            if current_state.depth > depth_level:
                loop_again = True
                continue

            #this goes down
            closed_list.append(current_state)

            # checking if the current state is a final state
            if current_state.isFinal():
                done = current_state
                break

            #generating all the children of the state
            children = generateAllChildren(current_state)

            #setting the children attibute of the node = to the generated list of children
            current_state.children = children

            #adding the list of children to the open list
            open_list.extend(children)

        if done:
          return done

        depth_level +=1
        print("depth level: " + str(depth_level))
    return None



def depthFirst():
    '''Not Complete'''
    #current_node = Tree
    global open_list
    global closed_list
    # generate the child state
    #child_state = generateChild(current_node.data)
    stoptime = time.time() + 60
    counter =0
    while len(open_list) > 0:

        if counter == 100:
            counter =0
            print("open list: " + str(len(open_list)) + "\nclose list: "+ str(len(closed_list)))
        counter += 1
        current_state = open_list.pop()
        #closed_list.append(current_state)

        if is_cycle(current_state):
            continue

        #print(len(open_list))
        #if current state is not null (i.e if a child exists
        if current_state:

            now = time.time()
            if now > stoptime:
                return current_state
                
            #check if it is the final state
            if current_state.isFinal():
                return current_state
            #if not, creat anew  tree node, fit the child state into it, set all the variables for the node, and set current node to that node
            else:

                children = generateAllChildren(current_state)
                open_list.extend(children)
                

        #if there are no child states in the current state
        else:
            '''
            #add the current state into the closed list
            closed_list.append(current_state)
            current_node = current_node.parent
            print("no")
            '''
def difference(child_list, open_list, closed_list):
    returning_list = list()
    for state in child_list:
        if state in open_list or state in returning_list:
            continue
        else:
            returning_list.append(state)
    return returning_list



def generateAllChildren(state):
    returning = list()
    global open_list
    global closed_list
    # iterating through all the possible moveUp actions
    for i in list(range(0, rows)):
        for j in list(range(0, column)):
            out = state.moveUp(i, j)

            if not out: continue


            returning.append(out)

    # iterating through all the possible moveDown actions
    for i in list(range(0, rows)):
        for j in list(range(0, column)):
            out = state.moveDown(i, j)

            if not out: continue

            returning.append(out)
    # iterating through all the possible moveRight actions
    for i in list(range(0, rows)):
        for j in list(range(0, column)):
            out = state.moveRight(i, j)

            if not out: continue


            returning.append(out)
    # iterating through all the possible moveLeft actions
    for i in list(range(0, rows)):
        for j in list(range(0, column)):
            out = state.moveLeft(i, j)

            if not out: continue

            returning.append(out)

    filtered_list = difference(returning,open_list,closed_list)
    #random.shuffle(filtered_list)
    return filtered_list


class State(object):
    '''State that represents the puzzle. It contains the tuple/array (array for now) and a list of changes applied to it'''

    def __init__(self, Data, log,parent,children, depth ):
        self.Data = Data
        self.log = log
        self.parent = parent
        self.children = children
        self.depth = depth

    def addLog(self, log):
        self.log.append(log)
    def printData(self):
        out = ""
        for row in self.Data:
            out += "\n"+ str(row)
        out +=  "\nDepth level: " + str(self.depth)
        return out

    def moveLeft(self,row, col):
        if (col % column) > 0:
            new_state = State(copy.deepcopy(self.Data),copy.deepcopy(self.log),self, list(), copy.deepcopy(self.depth +1) ) #making a deep copy of the state
            possibleMove = new_state.Data
            temp = possibleMove[row][col -1]  # for swapping positions
            possibleMove[row][col - 1] = possibleMove[row][col]
            possibleMove[row][col] = temp
            new_state.log.append((row, col, "left"))    #adding the chages to the log of the state (the slides said we should do so, we don't use them yet)
            return new_state    #returns the new child state

        else:
            return None

    def moveRight(self, row, col):
        if (col % column) < (column - 1):
            new_state = State(copy.deepcopy(self.Data),copy.deepcopy(self.log),self, list(), copy.deepcopy(self.depth +1) )
            possibleMove = new_state.Data
            temp = possibleMove[row][col + 1]  # for swapping positions
            possibleMove[row][col + 1] = possibleMove[row][col]
            possibleMove[row][col] = temp
            new_state.log.append((row, col, "right"))
            return new_state
        else:
            return None

    def moveDown(self, row, col):
        if row < rows -1:
            new_state = State(copy.deepcopy(self.Data),copy.deepcopy(self.log),self, list(), copy.deepcopy(self.depth +1) )
            possibleMove = new_state.Data
            temp = possibleMove[row +1][col]  # for swapping positions
            possibleMove[row +1][col] = possibleMove[row][col]
            possibleMove[row][col] = temp
            new_state.log.append((row, col, "down"))
            return new_state
        else:
            return None

    def moveUp(self, row, col):
        if row > 0 and row <= rows :
            new_state = State(copy.deepcopy(self.Data),copy.deepcopy(self.log),self, list(), copy.deepcopy(self.depth +1) )
            possibleMove = new_state.Data
            temp = possibleMove[row -1][col]  # for swapping positions
            possibleMove[row -1][col] = possibleMove[row][col]
            possibleMove[row][col] = temp
            new_state.log.append((row, col, "up"))
            return new_state
        else:
            return None

    def __eq__(self, state2):
        '''overwriting the equals method'''
        if self.Data == state2.Data:
            return True
        else:
            return False
    def __str__(self):
        out = ""
        for row in self.Data:
            out = out + str(row) +"\n"
        return out

    def isFinal(self):
        global goal
        if self.Data == goal:
            return True
        else:
            return False

def printHistory(state):#solution path
    nodes = list()

    #adding current node to the list
    nodes.append(state)

    #getting the parent of the node
    parent = state.parent

    #adding all the ancestors of the state to the list
    while parent:
        nodes.append(parent)
        parent = parent.parent

    #printing all the ancestors
    for state in list(reversed(nodes)):
        print("\n")
        state.printData()

def is_cycle(state):
    '''Checks if the state has the same representation in its line of parents'''
    ancestor = state.parent

    while ancestor:
        if state == ancestor:
            return True
        ancestor = ancestor.parent
    return False

def iterative_deepening_search():
    start_time = timer()
    depth = 1
    global open_list
    original_open_list = copy.deepcopy(open_list)

    while depth < 100:
        print("depth level: " + str(depth) + "time: " + str(timer() - start_time))
        open_list = copy.deepcopy(original_open_list)
        result = depth_limited_search(depth)
        if result:
            return result
        depth +=1
    return None

def depth_limited_search(n):
    global search_path
    global open_list
    result =None
    stoptime = time.time() + 5
    while len(open_list) >0:
        now = time.time()
        current_state = open_list.pop()
        search_path.append(current_state)#append
        if now > stoptime:
            return current_state
        if current_state.isFinal():
            return current_state
        if current_state.depth >n:
            result = None
        else:
            if not is_cycle(current_state):
                children = generateAllChildren(current_state)
                open_list.extend(children)
    return result

def print_search_pathdf():
    sPthat = search_path
    file = open("df.txt", 'w')
    for state in sPthat:
        file.write(state.printData())
        print("\n")
    file.close

def print_search_pathid():
    sPthat = search_path
    file = open("id.txt", 'w')
    for state in sPthat:
        file.write(state.printData())
        print("\n")
    file.close


column =3
rows = 3
goal = [[1,2,3],[4,5,6],[7,8,9]]
init = [[1,3,2], [4,6,5],[7,9,8]]
search_path = list() 

#example inputs
#[[2,3,5], [1,8,6],[9,7,4]]
#[[1,3,2], [5,8,6],[9,7,4]]
#[[2,3,5], [1,8,6],[9,7,4]]
#
init = [[2,3,5], [1,8,6],[9,7,4]]

if __name__ == '__main__':



    open_list = deque()
    closed_list = list()

    #starting intial state
    s = State(init,list(),None, list(),1)
    s.depth = 1

    #adding the inital state to the open list
    open_list.append(s)


    #####iterative_deepening
    ret = iterative_deepening_search()
    end = timer()

    if ret.Data == goal:
        printHistory(ret)
    else:
        
        print("overtime")
    print_search_pathid()#seach


#####depthfirst
    # ret = depthFirst()
    # end = timer()

    # if ret.Data == goal:
    #     printHistory(ret)
    # else:
    #     print("overtime")
    #print_search_pathdf()#seach
    

