import openpyxl
import os


os.chdir('d:\\Downloads')
wb = openpyxl.load_Workbook('Book1.xlsx')
type(wb)
