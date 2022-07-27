import React, { Component, Fragment } from "react";
import TodoItem from "./TodoItem";
import axios from 'axios';
import './style.css';

class TodoList extends Component {

  constructor(props) {
    super(props);
    this.state = {
      inputValue: 'Hello',
      list: []
    }
    this.handleInputChange = this.handleInputChange.bind(this)
    this.handleBtnClick = this.handleBtnClick.bind(this)
    this.handleItemDelete = this.handleItemDelete.bind(this)
  }

  // 在组件即将被挂载到页面的时刻自动执行
  componentWillMount(){
    console.log('componentWillMount');
  }

  render() {
    console.log('render')
    return (
      <Fragment>
        <div>
          <label htmlFor='insterArea'>输入内容</label>

          <input
            id='insterArea'
            className='input'
            value={this.state.inputValue}
            onChange={this.handleInputChange}
            />

          <button onClick={this.handleBtnClick}>提交</button>
        </div>
        <ul ref={(ul)=>{this.ul = ul}}>
          {this.getTodoItem()}
        </ul>
      </Fragment>
    )
  }

  //组件被挂载到页面之后执行
  componentDidMount(){
    console.log('componentDidMount');
    axios.get('/todolist.json')
      .then((res)=>{
        this.setState(()=>({
          list: [...res.data]
        }));
      }
      )
      .catch(()=>{alert('fail')})
  }

  //组件被更新之前，它会自动被执行. return false网页不会被更新
  shouldComponentUpdate(){
    console.log('shouldComponentUpdate');
    return true;
  }
  
  // 组件被更新之前，它会自动执行，但是他在shouldComponentUpdate之后
  // 如果shouldComponentUpdate返回true它才执行
  // 如果返回false, 这个函数就不会被执行了
  componentWillUpdate(){
    console.log('componentWillUpdate');
  }
  getTodoItem() {
    return this.state.list.map((item, index) => {
      return (
        <TodoItem
          key={index}
          content={item}
          index={index}
          deleteItem={this.handleItemDelete} />
        // <li 
        //   key={index} 
        //   onClick={this.handleItemDelete.bind(this, index)}
        //   dangerouslySetInnerHTML={{__html: item}}>
        //   {
        //   //{item}
        //   }
        // </li>

      )
    })
  }
  handleInputChange(e) {

    const value = e.target.value;
    this.setState(() => ({
      inputValue: value
    }))
    // this.setState({
    //   inputValue: e.target.value
    // })
  }
  handleBtnClick() {
    this.setState((prevState) => ({
      list: [...prevState.list, prevState.inputValue],
      inputValue: '',
    }), () => {
      console.log(this.ul.querySelectorAll('li').length)
    })
  }
  handleItemDelete(index) {
    // immutable
    // state 不允许做任何改变
    this.setState((prevState) => {
      const list = [...prevState.list];
      list.splice(index, 1);
      return { list }
    });
  }
}

export default TodoList