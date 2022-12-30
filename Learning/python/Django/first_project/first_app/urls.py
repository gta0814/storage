from django.urls import re_path as url
from first_app import views

urlpatterns = [
    url("", views.index, name='index'),
    url("", views.users, name='users'),
]