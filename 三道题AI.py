import copy


class player:
    a =0
    b =0
    tokens = None
    parent=None
    taken_tokens = None
    depth = 0
    token_list = list()
    children = list()

    def __init__(self, tokens, taken_tokens, token_list, depth):
        self.tokens = tokens
        self.taken_tokens = taken_tokens
        self.token_list = token_list
        self.depth = depth


    def run(self, tokens, taken_tokens, token_list, depth):
        if taken_tokens ==0:
            self.run_all_tree()
        else:
            if taken_tokens %2 ==0:
                self.max(tokens, taken_tokens, token_list, depth)
            else:
                self.min(tokens, taken_tokens, token_list, depth)

    def run_all_tree(self):

       return 0

    def firstMove(self):
        return 0

    def min(self, tokens, taken_tokens, token_list, depth):

        return 0

    def generate_children(self, tokens, taken_tokens, token_list, depth):
        last_move = token_list[len(token_list) - 1]
        factors_multiples = list()
        chilidren = list()
        #token_list_copy = copy.deepcopy(token_list)

        #finding the factors and multiples of the last move
        for i in range(tokens):
            if ( i % last_move == 0 ) or ( last_move % i == 0 ):
                factors_multiples.append(i)

        #creating the new children
        for num in factors_multiples:
            child = player(tokens, taken_tokens +1 , copy.deepcopy(token_list).append(i) ,depth)
            child.parent = self
            chilidren.append(child)


        return chilidren

    def max(self, tokens, taken_tokens, token_list, depth):

        return 0


if __name__ == '__main__':

    p1 = player()
    p1.run(7, 3, [1,4,2], 3)