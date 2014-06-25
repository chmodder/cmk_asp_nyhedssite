/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config)
{
    // Define changes to default configuration here. For example:
     config.language = 'Da';
     config.uiColor = '#c8c8c8';
    //Bootstrap
    //config.allowedContent = true;
    //config.bodyClass = 'content'; //class that body needs to refer to
     config.contentsCss = '//netdna.bootstrapcdn.com/bootswatch/3.0.0/flatly/bootstrap.min.css';

    config.htmlEncodeOutput = true;

    config.toolbarGroups = [
//{ name: 'document', groups: ['mode', 'document', 'doctools'] },
{ name: 'clipboard', groups: ['clipboard', 'undo'] },
{ name: 'editing', groups: ['find', 'selection', 'spellchecker'] },
/*{ name: 'forms' },*/
'/',
{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align'] },
{ name: 'links' },
{ name: 'insert' },
'/',
{ name: 'styles' },
{ name: 'colors' },
{ name: 'tools' }
//{ name: 'others' },
//{ name: 'about' }
    ];
};
