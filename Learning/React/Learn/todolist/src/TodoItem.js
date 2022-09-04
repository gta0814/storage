import React, { Component } from 'react';
import PropTypes from 'prop-types';

class TodoItem extends Component {

  constructor(props) {
    super(props);
    this.handleClick = this.handleClick.bind(this);
  }

  shouldComponentUpdate(nextProps, nextState){
    if(nextProps.content !== this.props.content){
      return true;
    }
    else{
      return false;
    }
  }
  render() {
    const {content, test} = this.props
    return <li onClick={this.handleClick}>
      {test} - {content}
    </li>
  }

  handleClick() {
    const {deleteItem, index} = this.props;
    deleteItem(index);
  }

  // 一个组件要从父组件接收参数
  // 只要父组件的render函数被“重新”执行了，子组件的这个生命周期函数就会被执行
  componentWillReceiveProps(){
    console.log('child componentWillReceiveProps');
  }

  // 当这个组件即将被从页面中剔除的时候，会执行
  componentWillUnmount(){
    console.log('child componentWillUnmount');
  }
}

TodoItem.propTypes = {
  test: PropTypes.string.isRequired,
  content: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
  deleteItem: PropTypes.func,
  index: PropTypes.number
}

TodoItem.defaultProps = {
  test: 'hello world'
}

export default TodoItem;