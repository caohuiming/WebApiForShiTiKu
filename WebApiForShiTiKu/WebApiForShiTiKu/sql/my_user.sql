/*
Navicat MySQL Data Transfer

Source Server         : MySQL57
Source Server Version : 50717
Source Host           : 127.0.0.1:3306
Source Database       : shitiku2

Target Server Type    : MYSQL
Target Server Version : 50717
File Encoding         : 65001

Date: 2017-09-17 18:56:38
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for my_user
-- ----------------------------
DROP TABLE IF EXISTS `my_user`;
CREATE TABLE `my_user` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `user_name` varchar(50) NOT NULL,
  `user_phone` varchar(50) NOT NULL,
  `user_pwd` varchar(50) NOT NULL,
  `c_t` datetime NOT NULL,
  `u_t` datetime NOT NULL,
  `is_deleted` int(11) NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of my_user
-- ----------------------------
INSERT INTO `my_user` VALUES ('1', 'admin', '13716978163', 'qHCRH9PWiaQ=', '2017-09-16 00:00:00', '2017-09-17 18:51:58', '0');
