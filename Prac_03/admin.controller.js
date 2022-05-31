//const Comment = require('../models/comment.model')
const User = require('../models/user.model')
const bcrypt = require('bcrypt-nodejs')

module.exports.changePassword = async (req, res) => {
    const candidate = await User.findOne({login: req.body.login})
    const isPasswordCorrect = bcrypt.compareSync(req.body.oldPassword, candidate.password)
    if(isPasswordCorrect){
        try {
            const salt = bcrypt.genSaltSync(10)
            const $set = {
                password: bcrypt.hashSync(req.body.password, salt)
            }
            const user = await User.findOneAndUpdate({login: req.body.login}, {$set}, {new: true})
            res.json(user)
        } catch (error) {
            res.status(500).json(error)
        }
    } else {
        res.status(409).json({message: 'Старый пароль неверный'})
    }
}

module.exports.getUsers = async (req, res) => {
    try {
        const users = await User.find().sort({date: -1})
        res.json(users)
    } catch (error) {
        res.status(500).json(error)
    }
}

module.exports.getAccounts = async (req, res) => {
    try {
        const accounts = await User.find();
        const activeAccounts = await User.find({"status": "active"});
        res.json({accounts, activeAccounts})
    } catch (error) {
        res.status(500).json(error)
    }
}

module.exports.banUser = async (req, res) => {
    try {
        const $set = {
            status: "banned"
        }
        await User.findOneAndUpdate({login: req.body.login}, {$set}, {new: true})
        res.json("Пользователь забанен")
    } catch (error) {
        res.status(500).json(error)
    }
}

module.exports.renameUser = async (req, res) => {
    const candidate = await User.findOne({login: req.body.id})
    if(candidate){
        res.status(409).json({message: 'Такой логин уже зарегистрирован'});
    } else {
        try {
            const $set = {
                login: req.body.login
            }
            await User.findOneAndUpdate({_id: req.body.id}, {$set}, {new: true})
            res.json("Логин пользователя обновлён")
        } catch (error) {
            res.status(500).json(error)
        }
    }
}