from django.urls import  re_path as url
from . import views

app_name = 'basic_app'

urlpatterns = [
    url("relative/", views.relative, name='relative'),
    url("other/", views.other, name = 'other'),
]